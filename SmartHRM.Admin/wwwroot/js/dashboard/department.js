import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCard = new huCard("rowCard");
    var totalDepartment = await getData("/Department/Statistic/Total");
    var card1 = {
        title: "Total Department",
        number: totalDepartment,
        unit: "Department",
        url: "/Employee/Department",
        icon: "bx bx-user",
        iconColor: "success"
    }
   
    newCard.add(card1)

    var employeeCountByDepartment = await getData("/Department/Statistic/EmployeeCount");

    // Chart: Display employee count statistics using a bar chart
    var chartLabels = Object.keys(employeeCountByDepartment);
    var chartData = Object.values(employeeCountByDepartment);

    var newChart = new huApex("apex-barchar", chartLabels, chartData);
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