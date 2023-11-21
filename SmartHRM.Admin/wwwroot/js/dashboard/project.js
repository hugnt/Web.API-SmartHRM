import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCard = new huCard("rowCard-project");
    var totalProject = await getData("/Project/Statistic/Total");
    var totalProjectTime = await getData("/Project/Statistic/TotalProjectTime");
    var card1 = {
        title: "Total Project",
        number: totalProject,
        unit: "project",
        url: "/Tasks/Project",
        icon: "bx bxs-bar-chart-square",
        iconColor: "success"
    }
    var card2 = {

        title: "Total Days of Project",
        number: totalProjectTime,
        unit: "days",
        url: "/Tasks/Project",
        icon: "bx bx-time",
        iconColor: "warning"
    }
    newCard.add(card1)
    newCard.add(card2)

    //TABLE
    var listData = await getList("/Project/Statistic/TopFastest/3");
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
            id: "leader_id",
            name: "Leader ID"
        },


        {
            id: "startedAt",
            name: "StartedAt",
            formatter: function (e) {
                return toDateTime(e)
            }
        },

        {
            id: "endedAt",
            name: "EndedAt",
            formatter: function (e) {
                return toDateTime(e)
            }
        }
    ];
    var newGrid = new huGrid("table-project", columns, listData);


    //CHART
    var dataObj = await getData("/Project/Statistic/Project");

    var chartLabels = ["NotStarted", "Progressing", "Finished"];
    var newChart = new huApex("apex-barchar-project", chartLabels, dataObj);
    newChart.barChart();

    var chartLabels2 = ["NotStarted", "Progressing", "Finished"];
    var newChart2 = new huApex("apex-piechart-project", chartLabels2, dataObj);
    newChart2.pieChart();

    var chartLabels3 = ["NotStarted", "Progressing", "Finished"];
    var newChart3 = new huApex("apex-donutchart-project", chartLabels3, dataObj);
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