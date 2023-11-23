import jwt_decode from './decodeJWT.js';
import './jqueryCookie.js'
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //GET TOKEN
    var accessToken = $.cookie('AccessToken');
    var decoded = jwt_decode(accessToken);

    const ACCOUNT_ID = decoded.Id;
    console.log(ACCOUNT_ID);

    const ACCOUNT_INFOR = await getData(`/Account/AccountInfor/${ACCOUNT_ID}`);
    console.log(ACCOUNT_INFOR);

    //ADD ROLE
    var expirationDate = new Date();
    expirationDate.setTime(expirationDate.getTime() + (3 * 60 * 60 * 1000)); // 3 giờ

    $.cookie("Role", ACCOUNT_INFOR.roleName.toString(), { expires: expirationDate, path: "/" });

    await setInforAccount(ACCOUNT_INFOR);
    
    async function setInforAccount(data) {
        $("#account-fullName").text(data.fullName);
        $("#account-role").text(data.roleName);
        var accountAvatar = `${API.IMAGE_URL}/avatar/${ACCOUNT_INFOR.avatar}`;
        checkImage(accountAvatar)
            .then(url => {
                $("#account-avatar").prop("src", url);
            })
            .catch(() => {
                $("#account-avatar").prop("src", `${API.IMAGE_URL}/avatar/user4.png`);
            });
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

    function checkImage(url) {
        return new Promise((resolve, reject) => {
            const img = new Image();
            img.onload = () => resolve(url);
            img.onerror = () => reject(url);
            img.src = url;
        });
    }

});