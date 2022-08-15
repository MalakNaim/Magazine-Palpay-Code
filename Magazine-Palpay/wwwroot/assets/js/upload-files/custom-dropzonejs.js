"use strict";
var KTDropzoneDemo = function () {
    var demo1 = function () {
       $('#kt_dropzone_2').dropzone({
           url: "/Orders/Attachments/Upload" + UploadUrl,
            paramName: "file", 
            maxFiles: 10,
            maxFilesize: 400, // MB
            addRemoveLinks: true,
            parallelUploads: 10,
            uploadMultiple: true,
            autoQueue: true,
            acceptedFiles: "image/*,application/pdf,.doc",
            accept: function (file, done) {
                if (file.name == "justinbieber.jpg") {
                    done("Naha, you don't.");
                } else {
                    done();
                }
           },
           init: function () {
               var thisDropzone = this;
               $.getJSON("/Orders/Attachments/Get/" + QueryUrl).done(function (Get) {
                   $.each(Get, function (key, element) {
                       var mockFile = {
                           name: "No Image Name",
                           size: element.id,
                           ID: element.id
                       };
                       var fileName = element.mimeType == "application/pdf" ? "pdf.svg" : element.fileName;
                       thisDropzone.emit("addedfile", mockFile);
                       thisDropzone.emit("thumbnail", mockFile, element.virtualPath + "" + fileName);
                   });
               });

               this.on("addedfile", function (file) {
                   var previewButton = Dropzone.createElement("<a class='btn btn-icon btn-outline-primary btn-circle btn-sm mr-2'><i  style='cursor: pointer;' class='flaticon-eye'></i></a>");
                   previewButton.addEventListener("click", function (e) {
                       e.preventDefault();
                       e.stopPropagation();
                       jQueryModalGet('/Orders/Attachments/Details/'+file.ID, 'Attachment Details');
                   });
                   file.previewElement.appendChild(previewButton);
               });

               this.on("addedfile", function (file) {
                   var removeButton = Dropzone.createElement("<button class='btn btn-icon btn-outline-danger btn-circle btn-sm mr-2 deleteItem'><i  style='cursor: pointer;' class='flaticon2-trash'></i></button>");
                   removeButton.addEventListener("click", function (e) {
                       e.preventDefault();
                       e.stopPropagation();
                       //jQueryModalConfirm('/Orders/Attachments/OnPostDelete?id=' + file.ID, 'Are you sure to delete this record ?');
                       $.get("/Orders/Attachments/OnPostDelete/" + file.ID, function (data) {
                           if (data.status = true) {
                               thisDropzone.removeFile(file);
                           } else { alert("Error while deleting file"); }
                       });
                   });
                    file.previewElement.appendChild(removeButton);
               });
           }
       });
       
    }
    return {
        init: function() {
            demo1();
        }
    };
}();
 KTUtil.ready(function() {
    KTDropzoneDemo.init();
 });

