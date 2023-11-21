import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCard = new huCard("timeKeepingCard");
    var numberEmployeeLate = await getData("/TimeKeeping/Statistic/GetNumberOnTimeEmployee/46");
    var card1 = {
        title: "Total Employee Late",
        number: numberEmployeeLate,
        unit: "people",
        url: "/TimeKeeping/TimeKeeper",
        icon: "bx bx-user",
        iconColor: "success"
    }
    var newCard2 = new huCard("timeKeepingCard2")
    var numberLate = await getData("/TimeKeeping/Statistic/GetNumberEmployeeNoWork/46")
    var card2 = {
        title: "Total Number Employee No Work",
        number: numberLate,
        unit: "people",
        url: "/TimeKeeping/TimeKeeper",
        icon: "bx bx-user",
        iconColor: "success"
    }
    newCard.add(card1)
    newCard.add(card2)

    //TABLE
    var listDataOnTime = await getList("/TimeKeeping/Statistic/GetUsuallyLate/5");
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
            name: "Name"
        }];
    var newGrid = new huGrid("list5Deduction", columns, listData);


    //CHART
    var dataObj = await getData("/TimeKeeping/Statistic/GetListOnTime/46");

    var chartLabels = ["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"];
    var newChart = new huApex("DetailChart", chartLabels, dataObj);
    newChart.barChart();


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
});