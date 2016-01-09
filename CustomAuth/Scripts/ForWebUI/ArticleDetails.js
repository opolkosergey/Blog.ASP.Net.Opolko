    var getTrashGly = function (element) {
        var id = $(element).attr("id");
        var butId = "trash_" + id;
        return document.getElementById(butId);
    };

var getEditGly = function (element) {
    var id = $(element).attr("id");
    var editId = "edit_" + id;
    return document.getElementById(editId);
};

function ShowGlyphs(element) {
    $(getTrashGly(element)).removeClass('hidden');
    $(getEditGly(element)).removeClass('hidden');
};

function HideGlyphs(element) {
    $(getTrashGly(element)).addClass('hidden');
    $(getEditGly(element)).addClass('hidden');
};

function CloseEdit(element) {
    var editBlockId = $(element).parent().attr("id");
    var editBlock = document.getElementById(editBlockId);
    editBlock.innerHTML = "";
};

function Edit(element) {
    var id = $(element).parent().parent().parent().attr("id");
    var commentText = $('#userComment').val();
    var data = id + '~' + commentText;

    $.get("/Comment/EditComment?data=" + data, function (response) {
        if (response === "ok") {
            $("#comment_" + id).html(commentText);
            $(".editing").empty();
        }
    });
};
    
function EditComment(element) {
    $(".editing").empty();
    var id = $(element).parent().parent().attr("id");
    var spanEditing = $("#commentEditInput_" + id);
        
    var c = $('#comment_' + id).html();
    spanEditing.html("<input type='text' name='UserComment' id='userComment' required value='' /><div class='glyphicon glyphicon-remove' onclick='CloseEdit(this)'></div><span class='btn btn-default' id='saveComment' onclick='Edit(this)'>Изменить</span>");
    $('#userComment').val(c);
};

function DeleteElement(element) {    
    var id = $(element).parent().parent().attr("id");
    //alert(id);
    $.get("/Comment/DeleteComment?id=" + id, function (response) {
        if (response === "ok") {
            //document.getElementById(id).remove();
            $('#' + id).remove();
        }
    });
};