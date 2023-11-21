import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCard = new huCard("rowCard-task");
    var totalTask = await getData("/Task/Statistic/Total");
    var totalTaskTime = await getData("/Task/Statistic/TotalTaskTime");
    var card1 = {
        title: "Total Task",
        number: totalTask,
        unit: "task",
        url: "/Tasks/TaskList",
        icon: "bx bx-task",
        iconColor: "success"
    }
    var card2 = {
        title: "Total Time",
        number: totalTaskTime,
        unit: "hours",
        url: "/Tasks/TaskList",
        icon: "bx bx-time",
        iconColor: "warning"
    }
    newCard.add(card1)
    newCard.add(card2)

    //TABLE
    var listData = await getList("/Task/Statistic/TopEarliest/3");
    var columns = [
        {
            id: "id",
            name: htmlText(`<div class="text-center">Id</div>`),
            sort: false,
            formatter: function (e) {
                return htmlText(`<div class="text-center">${e}</div>`)
            }
        },
        {
            id: "name",
            name: "Name",
            sort: true
        },

        {
            id: "status",
            name: "Status"
        },

        {
            id: "startTime",
            name: "StartTime",
            formatter: function (e) {
                return toDateTime(e)
            }
        },
        {
            id: "content",
            name: "Content"
        },
        {
            id: "description",
            name: "Description"
        }
    ];
    var newGrid = new huGrid("table-task", columns, listData);


    //CHART
    var dataObj = await getData("/Task/Statistic/Task");

    var chartLabels = ["NotStarted", "Progressing", "Finished"];
    var newChart = new huApex("apex-barchar-task", chartLabels, dataObj);
    newChart.barChart();

    var chartLabels2 = ["NotStarted", "Progressing", "Finished"];
    var newChart2 = new huApex("apex-piechart-task", chartLabels2, dataObj);
    newChart2.pieChart();

    var chartLabels3 = ["NotStarted", "Progressing", "Finished"];
    var newChart3 = new huApex("apex-donutchart-task", chartLabels3, dataObj);
    newChart3.donutChart();


    //AJAX
    async function getList(endPoint) {
        try {
            const res = await $.ajax({
                url: `${API.API_URL}${endPoint}`,
                type: "GET",
                contentType: "application/json",
                beforeSend: function (xhr) {
                    AJAXCONFIG.ajaxBeforeSend(xhr, false);
                }
            });
            if (res) {
                return res.filter(x => x.isDeleted === false);
            }
        } catch (e) {
            console.log(e);
            //AJAXCONFIG.ajaxFail(e);
        }
        finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }

    async function getData(endPoint) {
        try {
            const res = await $.ajax({
                url: `${API.API_URL}${endPoint}`,
                type: "GET",
                contentType: "application/json",
                beforeSend: function (xhr) {
                    AJAXCONFIG.ajaxBeforeSend(xhr, false);
                }
            });
            if (res) {
                return res;
            }
        } catch (e) {
            console.log(e);
            AJAXCONFIG.ajaxFail(e);
        }
        finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }

    //format dateTime
    function toDateTime(stringDateTime) {
        var now = new Date(stringDateTime);

        var day = now.getDate();
        var month = now.getMonth() + 1;
        var year = now.getFullYear();

        var hours = now.getHours();
        var minutes = now.getMinutes();

        var formattedDate = month + '/' + day + '/' + year;
        var formattedTime = hours + ':' + (minutes < 10 ? '0' + minutes : minutes);

        return formattedDate + '  ' + formattedTime;

    }
});