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
    let dateString = tdElems[2].innerText;
    let dateParts = dateString.split('.'); 
    let timeParts = dateParts[2].split(' '); 
    let dateOnly = `${dateParts[1]}/${dateParts[0]}/${timeParts[0]}`;
    let timeOnly = timeParts[1]; 
    let formattedDateTime = `${dateOnly} ${timeOnly}`; 
    let newDate = new Date(formattedDateTime); 
    console.log(tdElems);
    let eventObj = {
        id: tdElems[0].innerText,
        title: tdElems[1].innerText,
        start: newDate,
        end: newDate,
        backgroundColor: tdElems[4].innerText,
    };
    eventsArr.push(eventObj);
}
console.log(eventsArr);

let trAvailableSlots = availableSlotsTable.getElementsByTagName("tr");
for (let tr of trAvailableSlots) {

    let tdElems = tr.getElementsByTagName("td");
    let dateString = tdElems[4].innerText;
    let dateParts = dateString.split('.');
    let formattedDate = `${dateParts[1]}/${dateParts[0]}/${dateParts[2]}`;
    let newDate = new Date(formattedDate);

    let eventObj = {
        id: tdElems[0].innerText,
        start: tdElems[1].innerText,
        end: tdElems[2].innerText,
        service: tdElems[3].innerText,
        date: newDate,
        isavailable: tdElems[5].innerText,
        reservationId: tdElems[6].innerText,
    };
    availableSlotsArr.push(eventObj);
}

let trbusySlots = busySlotsTable.getElementsByTagName("tr");
for (let tr of trbusySlots) {

    let tdElems = tr.getElementsByTagName("td");
    let dateString = tdElems[4].innerText;
    let dateParts = dateString.split('.');
    let formattedDate = `${dateParts[1]}/${dateParts[0]}/${dateParts[2]}`;
    let newDate = new Date(formattedDate);

    let eventObj = {
        id: tdElems[0].innerText,
        start: tdElems[1].innerText,
        end: tdElems[2].innerText,
        service: tdElems[3].innerText,
        date: newDate,
        isavailable: tdElems[5].innerText,
        reservationId: tdElems[6].innerText,
    };
    busySlotsArr.push(eventObj);
}

function showSchedule(serviceId, reservationSlots) {
    var slots = reservationSlots;
    var existingSlotsContainer = document.querySelector('#slotsButtons');
    var existingSlotsContainer1 = document.querySelector('#slotsButtons');

    console.log(existingSlotsContainer);
    existingSlotsContainer.innerHTML = '';

    for (var i = 0; i < slots.length; i++) {
        var slot = slots[i];
        var slotElement = document.createElement('button');
        slotElement.textContent = slot.start + ' - ' + slot.end;

        if (slot.isavailable === 'False') {
            slotElement.classList.add('button-disabled');
             slotElement.onclick = (function(reservationSlotId) {
                return function() {
                    var confirmed = confirm('This date is already booked. Would you like to receive a notification when it becomes available?');
                    if (confirmed) {
                        SubscribeToSlot(reservationSlotId);
                     }
                }
            })(slot.id);
        } else {
            var formattedDate = slot.date.toLocaleDateString(undefined); 
            slotElement.onclick = (function(serviceId, formattedDate, slotId) {
                return function() {
                    NewReservation(serviceId, formattedDate, slotId);
                }
            })(selectedServiceId, formattedDate, slot.id);
        }
        existingSlotsContainer1.append(slotElement);
    }
    document.replace(existingSlotsContainer, existingSlotsContainer1);
}

function SubscribeToSlot(reservationSlotId) {
    $.ajax({
        type: "POST",
        url: "/Reservation1/SubscribeSlot",
        data: {
            reservationSlotId: reservationSlotId
        },
        success: function (response) {
            console.log("Subscription successful!");
            console.log(response);
            console.log("Subscription successful!");
        },
        error: function (error) {
                alert("You are already subscribed to this date.");
        }
    });
}
function NewReservation(serviceId, selectedDate, reservationSlotsId) {
    $.ajax({
        type: "GET",
        url: "/Reservation1/NewReservation",
        xhrFields: {
            withCredentials: true
        },
        data: {
            serviceId: serviceId,
            selectedDate: selectedDate,
            reservationSlotsId: reservationSlotsId
        },
        success: function (response) {
            console.log(response);
            $('#modalContent').html(response);
            $('#myModal').modal('show');
        },
        error: function (xhr, status, error) {
            if (xhr.status === 401) {
                window.location.href = "/Account/LoginPage";
            }
        }
    });
}


function setSelectedServiceId(serviceId) {
    selectedServiceId = serviceId;

    var v = document.getElementsByClassName("fc-scrollgrid-sync-inner");
    var tdElements = document.querySelectorAll('[data-date]');

    for (var i = 0; i < tdElements.length; i++) {
        if (tdElements[i].classList.contains("busy-slots")) {
            tdElements[i].classList.remove("busy-slots");
        }
    }

    for (var i = 0; i < tdElements.length; i++) {
        var date = tdElements[i].getAttribute('data-date'); 
        var formattedDate = new Date(date).toLocaleDateString(undefined);
        var slotsForDate = availableSlotsArr.filter(slot => slot.date.toLocaleDateString(undefined) === formattedDate && slot.service === selectedServiceId
        );

        if (!(slotsForDate.length > 0)) {
            tdElements[i].classList.add("busy-slots");
        }
    }
}

function refresh() {
    var tdElements = document.querySelectorAll('[data-date]');

    for (var i = 0; i < tdElements.length; i++) {
        if (tdElements[i].classList.contains("busy-slots")) {
            tdElements[i].classList.remove("busy-slots");
        }
    }
    var existingSlotsContainer = document.querySelector('#slotsButtons');
    existingSlotsContainer.innerHTML = '';
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
    locale: 'pl',
    events: eventsArr,
    dateClick: function (info) {
        selectedDate = info.date;

        let availableSlotsForSelectedDate = availableSlotsArr.filter(slot => slot.date.toLocaleDateString(undefined) === info.date.toLocaleDateString(undefined) && slot.service === selectedServiceId
        );

        let busySlotsForSelectedDate = busySlotsArr.filter(
            slot => slot.date.toLocaleDateString(undefined) === info.date.toLocaleDateString(undefined) && slot.service === selectedServiceId
        );

        let combinedSlotsForSelectedDate = availableSlotsForSelectedDate.concat(busySlotsForSelectedDate);
        showSchedule(selectedServiceId, combinedSlotsForSelectedDate);
    },
    eventClick: function (info) {
        let eventId = info.event.id; 
        $.ajax({
            url: '/Reservation1/ShowReservation', 
            method: 'GET',
            data: { id: eventId },
            success: function (data) {
                $('#modalContent').html(data);
                $('#myModal').modal('show');
                $('#confirmDeleteBtn').on('click', function () {
                    let indexToRemove = eventsArr.findIndex(event => event.id === eventId);
                    if (indexToRemove !== -1) {
                        eventsArr.splice(indexToRemove, 1);
                        let busySlotIndex = busySlotsArr.findIndex(slot => slot.reservationId === eventId);
                        if (busySlotIndex !== -1) {
                            busySlotsArr[busySlotIndex].isavailable = true;
                            let slotToMove = busySlotsArr[busySlotIndex];
                            availableSlotsArr.push(slotToMove);
                            busySlotsArr.splice(busySlotIndex, 1);
                        }
                        refresh();
                    } else {
                    }
                    calendar.getEventById(eventId).remove();
                    $('#deleteConfirmationModal').modal('hide');
                    $('#myModal').modal('hide');
                });
            }
        });
    },

});

calendar.render();
