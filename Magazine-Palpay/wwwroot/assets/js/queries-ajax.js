function getRegions(input,region,gov) {
    $(input).select2({
        placeholder: "اختر المدينة",
        ajax: {
            url: "/Lookups/GetLookups",
            contentType: "application/json; charset=utf-8",
            data: {
                id:region,
                entityId: "",
                government: gov
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            id: item.value,
                            text: item.text
                        };
                    }),
                };
            }
        }
    });
}

function getCities(input,city,region) {
    $(input).select2({
        placeholder: "اختر المدينة",
        ajax: {
            url: "/Lookups/GetLookups",
            contentType: "application/json; charset=utf-8",
            data: {
                id: city,
                entityId: region,
                government : ""
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            id: item.value,
                            text: item.text
                        };
                    }),
                };
            }
        }
    });
}

function getWorkNature(input,work) {
    $(input).select2({
        placeholder: "اختر طبيعة العمل",
        ajax: {
            url: "/Lookups/GetLookups",
            contentType: "application/json; charset=utf-8",
            data: {
                id: work,
                entityId: "",
                government: ""
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        $(input).append("<option value='0' selected>اختر طبيعة العمل</option>").trigger('change');
                        return {
                            id: item.value,
                            text: item.text
                        };
                    }),
                };
            }
        }
    });
}

function getGovernment(input,gov) {
    $(input).select2({
        placeholder: "اختر المحافظة",
        ajax: {
            url: "/Lookups/GetLookups",
            contentType: "application/json; charset=utf-8",
            data: {
                id: gov,
                entityId: "",
                government: ""
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        $(input).append("<option value='0' selected>اختر المحافظة</option>").trigger('change');
                        return {
                            id: item.value,
                            text: item.text
                        };
                    }),
                };
            }
            }
        });
}