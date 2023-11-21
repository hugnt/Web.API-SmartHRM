import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCard = new huCard("rowCard-Department");
    var totalDepartment = await getData("/Department/Statistic/Total");
    var card4 = {
        title: "Total Department",
        number: totalDepartment,
        unit: "Department",
        url: "/Employee/Department",
        icon: "bx bx-user",
        iconColor: "success"
    }
    
    //var employeeId = getEmployeeId();
    //var currentDepartment = await getCurrentDepartment(employeeId);
    //var newCard = new huCard("rowCard-Current");
    //var cardCurrent = {
    //    title: "Department",
    //    text: currentDepartment.name,
    //    unit: "Department",
    //    url: `/Department/CurrentDepartment/${employeeId}`,
    //    icon: "bx bx-building",
    //    iconColor: "info"
    //};
    //newCard.add(cardCurrent);
    newCard.add(card4)
    
    //Bar chart
    var employeeCountByDepartment = await getData("/Department/Statistic/EmployeeCount");

    // Chart: Display employee count statistics using a bar chart
    var chartLabels = Object.keys(employeeCountByDepartment);
    var chartData = Object.values(employeeCountByDepartment);

    var newChart = new huApex("barchar-Department", chartLabels, chartData);
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
    async function getCurrentDepartment(employeeId) {
        try {
            const res = await $.ajax({
                url: `${API.API_URL}/Department/CurrentDepartment/${employeeId}`,
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
        } finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }
});