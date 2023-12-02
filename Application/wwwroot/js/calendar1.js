let eventsArr = [];
let adminEventsArr = [];

let selectedId;
let selectedName;
let selectedTime;
let month;

let eventsTable = document.getElementById("eventsTable");

let adminEventsTable = document.getElementById("adminEventsTable");

let trElemsadmin = adminEventsTable.getElementsByTagName("tr");
for (let tr of trElemsadmin) {
    let tdElems = tr.getElementsByTagName("td");
    let eventObj = {
        id: tdElems[0].innerText,
        title: tdElems[1].innerText,
        start: new Date(tdElems[2].innerText),
        end: new Date(tdElems[3].innerText),
        backgroundColor: tdElems[4].innerText,
        textColor: 'red'
    };

    adminEventsArr.push(eventObj);
}

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
var elementsWithDataDate;

function showSchedule(id, name, time, button) {
     selectedId = id;
     selectedName = name;
    selectedTime = time;
    month = calendar.getDate().getMonth();
    let days = document.querySelectorAll('[data-date]');
    console.log(`ID: ${selectedId}, Name: ${selectedName}, Time: ${selectedTime}`);
    for (let i = 0; i < document.querySelectorAll('[data-date]').length; i++)
    {
        if (isAvailableDate(days[i].attributes['data-date'].nodeValue).length>0) {
            days[i].setAttribute('style', days[i].getAttribute('style') + 'background-color: white;');
        }
        else {
            days[i].setAttribute('style', 'background-color: rgba(200, 200, 200, 0.2); pointer-events: none;');

        }
    }

}

function isAvailableDate(date) {
    let availableHours = [];

    const [hours, minutes, seconds] = selectedTime.split(':').map(Number);
    const serviceDuration = (hours * 60 * 60) + (minutes * 60) + seconds;
    for (let hour = 8; hour <= 18; hour++) {
        let selectedDate = new Date(date);
        selectedDate.setHours(hour, 0, 0, 0);

        let endTime = new Date(selectedDate.getTime() + serviceDuration * 1000);
        let isAvailable = true;

        for (let i = 0; i < adminEventsArr.length; i++) {
            let eventStart = new Date(adminEventsArr[i].start);
            let eventEnd = new Date(adminEventsArr[i].end);
            if (
                (selectedDate < eventEnd && endTime > eventStart) ||
                (selectedDate >= eventStart && selectedDate < eventEnd) ||
                (endTime > eventStart && endTime <= eventEnd) ||
                (selectedDate < eventEnd && endTime > eventEnd) ||
                (endTime.getTime() > eventStart.getTime() && endTime.getTime() <= eventEnd.getTime()) ||
                (selectedDate.getTime() <= eventStart.getTime() && endTime.getTime() >= eventEnd.getTime()) ||
                (selectedDate <= eventStart && endTime >= eventEnd)
            ) {

                isAvailable = false;
                break;
            }
        }

        if (isAvailable) {
            availableHours.push({ start: selectedDate, end: endTime, serviceName: selectedName });
        }
    }
    return availableHours;
    }

    let availableHoursContainer = document.getElementById('availableHours');
    availableHoursContainer.innerHTML = '';

    for (let j = 0; j < availableHours.length; j++) {
        let button = document.createElement('button');
        button.innerHTML = availableHours[j].start.toLocaleTimeString() + ' - ' + availableHours[j].end.toLocaleTimeString();
        button.id = `button${j}`;

        button.onclick = function () {
            showSchedule(selectedId, selectedName, selectedTime, this);
            $.ajax({
                url: '/Reservation/NewReservation',
                type: 'GET',
                data: { start: formatDateToCustomString(availableHours[j].start), serviceName: selectedName },
                success: function (data) {
                    $('#newReservationContent').html(data);
                    let modal = new bootstrap.Modal(document.getElementById('newReservationModal'));
                    modal.show();
                }
            });
        };

        availableHoursContainer.appendChild(button);
    }

    console.log(availableHours);


let calendarEl = document.getElementById('calendar');

let calendar = new FullCalendar.Calendar(calendarEl, {
    initialView: 'dayGridMonth',
    initialDate: '2023-11-01',
    headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    
   

    events: eventsArr
,
    dateClick: function (info) {
        let availableHours = [];

        const [hours, minutes, seconds] = selectedTime.split(':').map(Number);
        const serviceDuration = (hours * 60 * 60) + (minutes * 60) + seconds;

        for (let hour = 8; hour <= 18; hour++) {
            let selectedDate = new Date(info.date);
            selectedDate.setHours(hour, 0, 0, 0); 

            let endTime = new Date(selectedDate.getTime() + serviceDuration * 1000);

            let isAvailable = true;

            for (let i = 0; i < adminEventsArr.length; i++) {
                let eventStart = new Date(adminEventsArr[i].start);
                let eventEnd = new Date(adminEventsArr[i].end);
                if (
                    (selectedDate < eventEnd && endTime > eventStart) ||
                    (selectedDate >= eventStart && selectedDate < eventEnd) ||
                    (endTime > eventStart && endTime <= eventEnd) ||
                    (selectedDate < eventEnd && endTime > eventEnd) ||
                    (endTime.getTime() > eventStart.getTime() && endTime.getTime() <= eventEnd.getTime()) || 
                    (selectedDate.getTime() <= eventStart.getTime() && endTime.getTime() >= eventEnd.getTime()) || 
                    (selectedDate <= eventStart && endTime >= eventEnd)
                ) {

                    isAvailable = false; 
                    break;
                }
            }

            if (isAvailable) {
                availableHours.push({ start: selectedDate, end: endTime, serviceName: selectedName });
            }
        }
    
        let availableHoursContainer = document.getElementById('availableHours');
        availableHoursContainer.innerHTML = ''; 

        for (let j = 0; j < availableHours.length; j++) {
            let button = document.createElement('button');
            button.innerHTML = availableHours[j].start.toLocaleTimeString() + ' - ' + availableHours[j].end.toLocaleTimeString();
            button.id = `button${j}`; 

            button.onclick = function () {
                showSchedule(selectedId, selectedName, selectedTime, this);
                $.ajax({
                    url: '/Reservation/NewReservation', 
                    type: 'GET',
                    data: { start: formatDateToCustomString(availableHours[j].start), serviceName: selectedName },
                    success: function (data) {
                        $('#newReservationContent').html(data);
                        let modal = new bootstrap.Modal(document.getElementById('newReservationModal'));
                        modal.show();
                    }
                });
            };

            availableHoursContainer.appendChild(button);
        }

        console.log(availableHours);
    },

    eventClick: function (info) {
        let eventId = info.event.id; 
        $.ajax({
            url: '/Reservation/ShowReservation', 
            method: 'GET',
            data: { id: eventId },
            success: function (data) {
                $('#newReservationContent').html(data);

                let modal = new bootstrap.Modal(document.getElementById('newReservationModal'));
                modal.show();
            }
        });
    },


     
  });



console.log(eventsArr);
calendar.render();

function formatDateToCustomString(date) {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const seconds = String(date.getSeconds()).padStart(2, '0');

    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
}