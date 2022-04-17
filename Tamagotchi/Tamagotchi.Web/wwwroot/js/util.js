let Util = {
    isEmailAddress(val) {
        return /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/.test(val) || /w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)*/.test(val);
    },
    head: arr => arr[0],
    last: arr => arr[arr.length - 1],
    pull: (arr, ...args) => {
        let pulled = arr.filter((v, i) => !args.includes(v));
        arr.length = 0;
        pulled.forEach(v => arr.push(v));
    },
    remove: (arr, func) => Array.isArray(arr) ? arr.filter(func).reduce((acc, val) => {
        arr.splice(arr.indexOf(val), 1);
        return acc.concat(val);
    }, []) : [],
    contains: function (arr, item) {
        let i = arr.length;
        while (i--) {
            if (arr[i] === item) {
                return true;
            }
        }
        return false;
    },
    injectViewAsync: function injectViewAsync(model, injectInView, requestUrl, {
        typeRequest = "Post",
        isClose = true,
        isKeepContent = false,
        serializeForm = "",
        loadingIdentify = "",
        onSuccessComplete = function () {
        },
        showToasts = true
    } = {}) {
        console.log(model);
        console.log($(serializeForm).serialize());

        let loadView = loadingIdentify === "" ? injectInView : loadingIdentify;

        $(loadView).prop("disabled", true);
        $(loadView).attr("data-original-text", $(loadView).html());
        $(loadView).html('<i class="spinner-border spinner-border-sm"></i> Loading ...');

        $.ajaxSetup({ cache: true });
        $.ajax({
            async: true,
            type: typeRequest,
            url: requestUrl,
            data: model !== null ? model : $(serializeForm).serialize(),
            beforeSend: function (data) {
                Pace.start();
            },
            complete: function (xdata) {
                Pace.stop();
            },
            error: function (xhr, status, error) {
                toastr.error(xhr.status + ":  " + xhr.statusText);
            },
            success: function (html) {
                $(loadView).prop("disabled", false);
                $(loadView).html($(loadView).attr("data-original-text"));
                if (isClose) {
                    Util.closeModal();
                    $(injectInView).html(html);

                    let form = $(serializeForm).serialize();
                    if (form !== "" || model !== null && showToasts)
                        toastr.success("Successfully operation!!!");
                } else if (isKeepContent) {
                    $(injectInView).html(html);
                }
                else {
                    Util.showModal(html);
                }

                onSuccessComplete();
            },
        });
    },

    makeRequest: function makeRequest(model, requestUrl, {
        typeRequest = "Post",
        serializeForm = "",
        onSuccessComplete = function () {
        }
    } = {}) {
        $.ajaxSetup({ cache: true });
        $.ajax({
            async: true,
            type: typeRequest,
            url: requestUrl,
            data: model !== null ? model : $(serializeForm).serialize(),
            beforeSend: function (data) {
                Pace.start();
            },
            complete: function (xdata) {
                Pace.stop();
            },
            error: function (xhr, status, error) {
                toastr.error(xhr.status + ":  " + xhr.statusText);
            },
            success: function () {
                if ($(serializeForm).serialize() !== "" || model !== null)
                    toastr.success("Successfully operation!!!");
                onSuccessComplete();
            },
        });
    },

    editModal: function editModal(model, requestUrl, size, typeRequest = 'Post') {
        $.ajaxSetup({ cache: true });
        $.ajax({
            async: true,
            type: typeRequest,
            url: requestUrl,
            data: model,
            beforeSend: function (data) {
                Pace.start();
            },
            complete: function (data) {
                Pace.stop();
            },
            success: function (html) {
                Util.showModal(html, size);
            },
        });
    },
    confirm: function confirm(model, reloadView, requestUrl) {
        $.confirm({
            title: 'Delete?',
            content: 'Please confirm this action',
            autoClose: 'cancel|5000',
            theme: 'modern',
            buttons: {
                confirm: {
                    btnClass: 'btn-blue',
                    action: function () {
                        Util.injectViewAsync(model, reloadView, requestUrl)
                    }
                },
                cancel: {
                    btnClass: 'btn-danger',
                    action: function () {
                    }
                }
            }
        });
    },
    confirmWithoutInject: function confirmWithoutInject(model, requestUrl, {
        onSuccessComplete = function () { },
        confirmModal = {
            title: 'Delete?',
            content: 'Please confirm this action'
        }
    } = {}) {
        $.confirm({
            title: confirmModal.title,
            content: confirmModal.content,
            autoClose: 'cancel|5000',
            theme: 'modern',
            buttons: {
                confirm: {
                    btnClass: 'btn-blue',
                    action: function () {
                        if (requestUrl === null) {
                            onSuccessComplete()
                        }
                        else {
                            Util.makeRequest(model, requestUrl, {
                                onSuccessComplete: function () {
                                }
                            })
                        }
                    }
                },
                cancel: {
                    btnClass: 'btn-danger',
                    action: function () {
                    }
                }
            }
        });
    },
    showModal: function show(html, size) {
        $("#viewModals_dialog").addClass(size);
        $("#viewModals_modalBody").html(html);
        $("#viewModals").modal("show");
    },
    closeModal: function closeModal() {
        $("#viewModals").modal("hide");
        $("body").removeClass("modal-open");
        $(".modal-backdrop").remove();
    },

    showTable: function showTableIn(elementId, order) {
        $(elementId).dataTable({
            "columnDefs": [{ targets: 'no-sort', orderable: order }],
            "paging": true,
            "lengthChange": true,
            "searching": true,
            "ordering": order,
            "info": true,
            "autoWidth": false,
            "responsive": true,
            "retrieve": true
        });
    },

    showServerSideTable: function showServerSideTableIn(elementId, action, columns, order, exportData = 'lfrtip') {
        $(elementId).dataTable({
            "paging": true,
            "lengthChange": true,
            "searching": true,
            "lengthChange": false,
            "responsive": true,
            "columnDefs": [{ targets: 'no-sort', orderable: order }],
            "ordering": order,
            "info": true,
            "autoWidth": false,
            "responsive": true,
            "retrieve": true,
            "processing": true,
            "serverSide": true,
            "dom": exportData,
            "buttons": [
                {
                    "extend": 'csv',
                    "text": '<i class="fa fa-file-csv" style="color: white;"></i>',
                    "titleAttr": 'csv',
                    "action": window.export
                },
                {
                    "extend": 'excel',
                    "text": '<i class="fa fa-file-excel" style="color: white;"></i>',
                    "titleAttr": 'excel',
                    "action": window.export
                }
            ],
            "ajax": {
                "url": action,
                "type": "POST",
            },
            "columns": columns
        });
    },

    tinymceInit: function tinymceInitIn(selector) {
        tinymce.remove(selector);
        tinymce.init({
            selector: selector,
            branding: false,
            theme: "silver",
            plugins: [
                "advlist autolink link image lists charmap print preview hr anchor pagebreak",
                "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                "save table contextmenu directionality emoticons template paste textcolor"
            ],
            toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons",
            automatic_uploads: true,
            file_picker_callback: function (cb, value, meta) {
                var input = document.createElement('input');
                input.setAttribute('type', 'file');
                input.setAttribute('accept', 'image/*');
                input.onchange = function () {
                    var file = this.files[0];
                    var reader = new FileReader();
                    reader.onload = function () {
                        var id = 'blobid' + (new Date()).getTime();
                        var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                        var base64 = reader.result.split(',')[1];
                        var blobInfo = blobCache.create(id, file, base64);
                        blobCache.add(blobInfo);
                        cb(blobInfo.blobUri(), { title: file.name });
                    };
                    reader.readAsDataURL(file);
                };
                input.click();
            }
        });
    },

    imagePreview: function imagePreview(inputSelector, imgSelector) {
        if (inputSelector.files && inputSelector.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(imgSelector).attr('src', e.target.result);
            }
            reader.readAsDataURL(inputSelector.files[0]);
        }
    },

    absoluteWebUrl: function absoluteUrl(url) {
        if (!url)
            return " ";
        else if (url.startsWith("http"))
            return url;
        else
            return "http://".concat(url);
    }
};
