
export function ajaxBeforeSend(xhr, header){
    $(".bodyContent").css("display", "none");
    $(".loading").css("display", "block");
    header&&xhr.setRequestHeader("Authorization", "Bearer your-token");
}

export function ajaxAfterSend(){
    $(".bodyContent").css("display", "block");
    $(".loading").css("display", "none");
}

export function ajaxSuccessful(){
    $(".bodyContent").css("display", "block");
    $(".loading").css("display", "none");
}

export function ajaxFail(error){
    $(".loading").css("display", "none");
    $(".bodyContent").css("display", "block");
    localStorage.setItem("ERROR", JSON.stringify(error));
    //window.location.href = "/Error"
}

export function ajaxComplete(){
    $(".loading").css("display", "none");
    $(".bodyContent").css("display", "block");
}
