

$(document).ready(function () {

   

    $(".comentar button").click(function () {
           var params = $.parseJSON(document.forms[0].hdnparametro.value);                
        if (document.forms[0].txtcomentar.value.trim() == "")
            return false;

        $.ajax({
            type: "GET",
            url: "fn.aspx",
            data: { fn: params.fn, pid: params.pid, cmm: document.forms[0].txtcomentar.value },
            success: comEnviado

        });

        return false;
    });


    $("a[data-pid]").click(function () {

        $.ajax({
            type: "GET",
            url: this.href,
            success: segPublicacion

        });

        return false;
    });

    $("a[data-suid]").click(function () {

        $.ajax({
            type: "GET",
            url: this.href,
            success: segUsuario

        });

        return false;
    });
});



function comEnviado(data) {

    document.forms[0].txtcomentar.value = "";
    $("#dvComentarios").append(data);
    document.body.scrollTop = document.body.scrollHeight;
}


function segPublicacion(dat) {

    var data = $.parseJSON(dat);
    $("#dvMessage").html(data.message);

    setTimeout(function() {  $("#dvMessage").html("");}, 5000);
  
}

function segUsuario(dat) {

       var data = $.parseJSON(dat);
    $("#dvMessage").html(data.message);

    setTimeout(function() {  $("#dvMessage").html("");}, 5000);
}