import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCardDeduction = new huCard("employeeCard");
    var totalDeduction1 = await getData("/Deduction/Statistic/GetAmountDeduction");
    var cardDeduction1 = {
        title: "Total Deduction",
        number: totalDeduction1,
        unit: "Deduction",
        url: "/Salary/DeductionList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    
    var totalDeduction2 = await getData("/Deduction/Statistic/GetTotalAmountDeduction");
    var cardDeduction2 = {
        title: "Total Amount Deduction",
        number: totalDeduction2,
        unit: "VND",
        url: "/Salary/DeductionList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    newCardDeduction.add(cardDeduction1)
    newCardDeduction.add(cardDeduction2)


    //TABLE
    var listDataDeduction = await getList("/Deduction/Statistic/GetTopDeduction/5");
    var columnsDeduction = [
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
            name: "Deduction Name"
        },
        {
            id: "amount",
            name: "Amount",
        },
        {
            id: "note",
            name: "Note",
        }];
    var newGridDeduction = new huGrid("list5Deduction", columnsDeduction, listDataDeduction);

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