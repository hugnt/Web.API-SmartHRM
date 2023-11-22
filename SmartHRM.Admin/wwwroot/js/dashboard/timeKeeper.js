import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCardTime = new huCard("timeKeepingCard");
    var numberEmployeeTime1 = await getData("/TimeKeeping/Statistic/GetNumberOnTimeEmployee/46");
    var cardTime1 = {
        title: "Total Employee Late",
        number: numberEmployeeTime1,
        unit: "people",
        url: "/TimeKeeping/TimeKeeper",
        icon: "bx bx-user",
        iconColor: "success"
    }
    var numberEmployeeTime2 = await getData("/TimeKeeping/Statistic/GetNumberEmployeeNoWork/46")
    var cardTime2 = {
        title: "Total Number Employee No Work",
        number: numberEmployeeTime2,
        unit: "people",
        url: "/TimeKeeping/TimeKeeper",
        icon: "bx bx-user",
        iconColor: "success"
    }
    newCardTime.add(cardTime1)
    newCardTime.add(cardTime2)

    //TABLE
    var listDataTime = await getList("/TimeKeeping/Statistic/GetUsuallyLate/5");
    var columnsTime = [
        {
            id: "timesName",
            name: "Employee Name"
        },
        {
            id: "timesCount",
            name: "Times"
        }];
    var newGridTime = new huGrid("list5Late", columnsTime, listDataTime);


    //CHART
    var dataTime = await getData("/TimeKeeping/Statistic/GetListOnTime/46");

    var chartTime = ["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"];
    var newChartTime = new huApex("DetailChartTimeKeeping", chartTime, dataTime);
    newChartTime.barChart();


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