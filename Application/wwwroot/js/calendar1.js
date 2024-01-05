let eventsArr = [];
let availableSlotsArr = [];
let busySlotsArr = [];

let selectedId;
let selectedName;
let selectedTime;
let month;
let selectedServiceId;
let selectedDate;

let eventsTable = document.getElementById("eventsTable");
let availableSlotsTable = document.getElementById("availableSlots");
let busySlotsTable = document.getElementById("busySlots");

let trElems = eventsTable.getElementsByTagName("tr");
for (let tr of trElems) {
    let tdElems = tr.getElementsByTagName("td");
    let eventObj = {
        id: tdElems[0].innerText,
        title: tdElems[1].innerText,
        start: new Date(tdElems[2].innerText),
        end: new Date(tdElems[3].innerText),
        backgroundColor: tdElems[4].innerText,
    };
    eventsArr.push(eventObj);
}

let trAvailableSlots = availableSlotsTable.getElementsByTagName("tr");
for (let tr of trAvailableSlots) {
    let tdElems = tr.getElementsByTagName("td");
    let eventObj = {
        id: tdElems[0].innerText,
        start: tdElems[1].innerText,
        end: tdElems[2].innerText,
        service: tdElems[3].innerText,
        date: new Date(tdElems[4].innerText),
    };
    availableSlotsArr.push(eventObj);
}

let trbusySlots = busySlotsTable.getElementsByTagName("tr");
for (let tr of trbusySlots) {
    let tdElems = tr.getElementsByTagName("td");
    let eventObj = {
        id: tdElems[0].innerText,
        start: tdElems[1].innerText,
        end: tdElems[2].innerText,
        service: tdElems[3].innerText,
        date: new Date(tdElems[4].innerText),
    };
    busySlotsArr.push(eventObj);
}

function showSchedule(serviceId, serviceName, serviceTime, reservationSlots) {
    var slots = reservationSlots;
    var existingSlotsContainer = document.querySelector('#slotsButtons');

    for (var i = 0; i < slots.length; i++) {
        var slot = slots[i];

        var slotElement = document.createElement('button');
        slotElement.textContent = slot.start + ' - ' + slot.end;

        if (slot.isavailable === false) {
            slotElement.classList.add('button-disabled');
        }
        else {
            var formattedDate = slot.date.toLocaleDateString('en-GB'); // Formatowanie daty
            slotElement.onclick = (function(serviceId, formattedDate, slotId) {
                return function() {
                    NewReservation(serviceId, formattedDate, slotId);
                }
            })(selectedServiceId, formattedDate, slot.id);
        }
        existingSlotsContainer.appendChild(slotElement);
    }
}

function NewReservation(serviceId, selectedDate, reservationSlotsId) {
    console.log(serviceId);
    console.log(selectedDate);
    console.log(reservationSlotsId);

    $.ajax({
        type: "GET",
        url: "/Reservation1/NewReservation",
        data: {
            serviceId: serviceId,
            selectedDate: selectedDate,
            reservationSlotsId: reservationSlotsId
        },
        success: function (response) {
            $('#modalContent').html(response); // Wstrzyknięcie częściowego widoku do modala
            $('#myModal').modal('show'); // Wyświetlenie modala
        },
        error: function (error) {
            console.error("Wystąpił błąd:", error);
        }
    });
}

function setSelectedServiceId(serviceId) {
    selectedServiceId = serviceId;

    var v = document.getElementsByClassName("fc-scrollgrid-sync-inner");
    var tdElements = document.querySelectorAll('td.fc-day'); // Pobranie elementów td

    for (var i = 0; i < tdElements.length; i++) {
        if (tdElements[i].classList.contains("busy-slots")) {
            tdElements[i].classList.remove("busy-slots");
        }
    }

    for (var i = 0; i < tdElements.length; i++) {
        var date = tdElements[i].getAttribute('data-date'); // Pobranie wartości atrybutu data-date
        var formattedDate = date.split('-').reverse().join('/');

        var slotsForDate = availableSlotsArr.filter(slot => {
            return slot.date.toLocaleDateString('en-GB') === formattedDate && slot.service === selectedServiceId;
        });

        if (!(slotsForDate.length > 0)) {
            tdElements[i].classList.add("busy-slots");
        }
    }
}

let calendarEl = document.getElementById('calendar');
let calendar = new FullCalendar.Calendar(calendarEl, {
    initialView: 'dayGridMonth',
    initialDate: '2024-01-01',
    headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek'
    },
    events: eventsArr,
    dateClick: function (info) {
        selectedDate = info.date;
        console.log(availableSlotsArr);
        console.log("Selected service ID: ", selectedServiceId);
        console.log("Info date: ", info.date);
        console.log("Available slots array: ", availableSlotsArr);
        console.log("Busy slots array: ", busySlotsArr);

        let availableSlotsForSelectedDate = availableSlotsArr.filter(slot => {
            return slot.date.toLocaleDateString('en-GB') === info.date.toLocaleDateString('en-GB');
        });

        let busySlotsForSelectedDate = busySlotsArr.filter(slot => {
            return slot.date.toLocaleDateString('en-GB') === info.date.toLocaleDateString('en-GB');
        });

        console.log(availableSlotsForSelectedDate);
        console.log(busySlotsForSelectedDate);

        showSchedule(selectedServiceId, 'Service Name', 'Service Time', availableSlotsForSelectedDate);
        showSchedule(selectedServiceId, 'Service Name', 'Service Time', busySlotsForSelectedDate);
    },
    eventClick: function (info) {
        let eventId = info.event.id;
    },

});



console.log(eventsArr);
calendar.render();
