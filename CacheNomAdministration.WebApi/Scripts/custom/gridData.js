var gridData = (function () {
    $(document).ready(function () {
        let grid = $("#totalizerGrid").data("kendoGrid");

        $("#apply").click(function () {
            if (grid) {
                grid.dataSource.read();
            }
        })
    });

    function addAntiForgeryToken(data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    };

    function getControlPointId() {

        let select = $("#ControlPointID");
        let value;
        if (select.length > 0) {
            value = parseInt(select.val());
        }
        var result = { "ControlPointID": value };
        return addAntiForgeryToken(result);
    }

    function hideNoItemsAlert() {
        $('#no-data-alert').fadeTo(2000, 500).slideUp(500, function () {
            $('#no-data-alert').slideUp(500);
        });
    }

    return {
        getControlPointId: getControlPointId,
        hideNoItemsAlert: hideNoItemsAlert
    }
})();