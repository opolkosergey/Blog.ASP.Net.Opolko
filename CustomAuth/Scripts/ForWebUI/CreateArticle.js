
    $(document).ready(function () {
        var visibleText = $('#inputFileVisibleText');
        var hidden = $('#inputFileHidden');
        var visibleBtn = $('#inputFileVisibleBtn');
        visibleBtn.on('click', function () {
            hidden.click();
        })
        hidden.on('change', function () {
            var fileName = $(this).val().split('\\');
            fileName = fileName[fileName.length - 1];
            visibleText.val(fileName);
        });
    });