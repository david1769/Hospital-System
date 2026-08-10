window.calendarFilters = {
    doctorId: "",
    departmentId: ""
};

window.updateCalendarFilters = function (doctorId, departmentId) {
    window.calendarFilters.doctorId = doctorId || "";
    window.calendarFilters.departmentId = departmentId || "";
    console.log("Filters updated:", window.calendarFilters);
};

window.initFullCalendar = function (dotNetHelper, apiBase) {
    console.log("initFullCalendar called");

    function tryInit() {
        const calendarEl = document.getElementById('calendar');
        if (!calendarEl || !calendarEl.isConnected) {
            console.log("Calendar element not ready – retrying");
            setTimeout(tryInit, 100);
            return;
        }

        const calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'timeGridWeek',
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            height: 'auto',
            slotMinTime: '07:00:00',
            slotMaxTime: '19:00:00',
            editable: true,
            selectable: true,
            events: function (fetchInfo, successCallback, failureCallback) {
                let url = apiBase + '/api/Appointment?start=' + fetchInfo.start.toISOString() +
                    '&end=' + fetchInfo.end.toISOString();

                if (window.calendarFilters && window.calendarFilters.doctorId) {
                    url += '&doctorId=' + encodeURIComponent(window.calendarFilters.doctorId);
                }
                if (window.calendarFilters && window.calendarFilters.departmentId) {
                    url += '&departmentId=' + encodeURIComponent(window.calendarFilters.departmentId);
                }

                console.log("🔄 Fetching events with URL:", url);

                fetch(url)
                    .then(response => {
                        console.log("📡 API Status:", response.status);
                        if (!response.ok) {
                            return response.text().then(text => {
                                throw new Error(`HTTP ${response.status}: ${text}`);
                            });
                        }
                        return response.json();
                    })
        

                        .then(data => {
                            console.log("Raw API data:", data);

                            let items = [];
                            if (Array.isArray(data)) items = data;
                            else if (data && data.data) items = data.data;
                            else if (data && data.Data) items = data.Data;
                            else items = [];

                            console.log(`Found ${items.length} appointments from API`);

                            const events = items.map(a => {
                                console.log("Processing appointment:", a);

                                const id = a.id ?? a.Id;

                                // Use the title that the API already built
                                let title = a.title;
                                if (!title) {
                                    const patient = a.patientName ?? a.PatientName ?? '';
                                    const doctor = a.doctorName ?? a.DoctorName ?? '';
                                    title = patient && doctor ? `${patient} - ${doctor}` : (patient || doctor || 'Appointment');
                                }

                                // Use the 'start' field that the API provides
                                let start = null;
                                const startRaw = a.start ?? a.Start ?? a.appointmentDate ?? a.AppointmentDate;
                                if (startRaw) {
                                    start = new Date(startRaw).toISOString();
                                }

                                console.log(`→ Event ID ${id} | Title: "${title}" | Start: ${start}`);

                                return {
                                    id: id,
                                    title: title,
                                    start: start,
                                    backgroundColor: a.backgroundColor || "#3788d8",
                                    extendedProps: a.extendedProps || {
                                        patientName: a.patientName ?? a.PatientName,
                                        doctorName: a.doctorName ?? a.DoctorName,
                                        notes: a.notes ?? a.Notes,
                                        followUpRequired: a.followUpRequired ?? a.FollowUpRequired
                                    }
                                };
                            });

                            console.log("Final events sent to FullCalendar:", events);
                            successCallback(events);
                        })
                    .catch(err => {
                        console.error("❌ Failed to load appointments:", err);
                        failureCallback(err);
                    });
            },
            eventClick: (info) => dotNetHelper.invokeMethodAsync('OnEventClick', info.event.id),
            dateClick: (info) => dotNetHelper.invokeMethodAsync('OnDateClick', info.dateStr)
        });

        // Global helpers
        window.FullCalendarInstance = calendar;
        window.addCalendarEvent = function (evt) {
            if (window.FullCalendarInstance) {
               
                console.log('Event added:', evt);
            }
        };
        window.refetchCalendar = function () {
            if (window.FullCalendarInstance) {
                window.FullCalendarInstance.refetchEvents();
            }
        };
        window.showDialog = (dialogElement) => dialogElement?.showModal();
        window.closeDialog = (dialogElement) => dialogElement?.close();

        calendar.render();
        console.log("Calendar rendered");
    }

    tryInit();
};
