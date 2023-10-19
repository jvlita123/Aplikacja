
let eventsArr = [];

let eventsTable = document.getElementById("eventsTable");

let trElems = eventsTable.getElementsByTagName("tr");
for (let tr of trElems) {
    let tdElems = tr.getElementsByTagName("td");
    let eventObj = {
        id: tdElems[0].innerText,
        title: tdElems[1].innerText,
        start: tdElems[2].innerText.toString(),
        backgroundColor: '#378006',
       // display: "background"
    };
    
    eventsArr.push(eventObj);
}

    let calendarEl = document.getElementById('calendar');

    let calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        initialDate: '2023-09-07',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        
        events: eventsArr,

        dateClick: function (date, allDay, jsEvent, view) {

            for (let i = 0; i < eventsArr.length; i++) {
                if (date.dateStr.toString("yyyy-MM-dd") == eventsArr[i].start.toString("yyyy-MM-dd")) {
                    alert('Date: ' + date.dateStr + 'is not available');
                }
                else {
                    alert('Date: ' + date.dateStr + 'is available');
                }
            }
        },


    });
    console.log(eventsArr);
    calendar.render();
    