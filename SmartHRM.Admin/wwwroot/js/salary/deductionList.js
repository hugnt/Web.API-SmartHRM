import { htmlText, huGrid } from '../hugrid.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    var listData = await getList("/Deduction");
    console.log(listData);
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
            id:"name",
            name: "Deduction Name",
            sort: true,
            /*data: function (e) {
                    return htmlText(`
                    <div class= "d-flex align-items-center" >
                        <div class="flex-shrink-0 me-3">
                            <div class="avatar-sm bg-light rounded p-1">
                                <img src="" alt="" class="img-fluid d-block">
                            </div >
                        </div>
                        <div class="flex-grow-1">
                            <h5 class="fs-14 mb-1">
                                <a href="apps-ecommerce-product-details.html" class="text-body">${e.fullName}</a>
                            </h5>
                            <p class="text-muted mb-0">Department : <span class="fw-medium">${e.departmentId}</span></p>
                        </div >
                    </div > `)
             }*/
        },
        {
            id: "amount",
            name: "Amount"
        },
        {
            id: "note",
            name: "Note"
        },
        {
            id: 'Action',
            name: htmlText('<div class="text-center">Action</div>'),
            sort: false,
            formatter: function (e, t) {
                //t = (new DOMParser).parseFromString(t._cells[0].data.props.content, "text/html").body.querySelector(".checkbox-product-list .form-check-input").value;
                return htmlText(`
            <div class="dropdown text-center">
                <button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="ri-more-fill"></i></button>
                <ul class="dropdown-menu dropdown-menu-end">
                    <li>
                        <a class="dropdown-item" data-id=${t._cells[1].data}>
                        <i class="ri-eye-fill align-bottom me-2 text-muted"></i> View</a>
                    </li>
                    <li>
                        <a class="dropdown-item" data-id=${t._cells[1].data}>
                        <i class="ri-pencil-fill align-bottom me-2 text-muted"></i> Edit</a>
                    </li>
                    <li class="dropdown-divider"></li>
                    <li>
                        <a class="dropdown-item " href="#" data-id=${t._cells[1].data} data-bs-toggle="modal" data-bs-target="#removeItemModal">
                            <i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Delete</a>
                    </li>
                </ul>
            </div>`)
            }
        }];

    var newGrid = new huGrid("table-product-list-all", columns, listData);
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
            if (res && res.length > 0) {
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