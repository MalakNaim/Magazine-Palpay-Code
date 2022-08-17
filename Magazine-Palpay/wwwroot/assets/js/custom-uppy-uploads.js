var uppy = new Uppy.Core(
    {
        debug: true,
        autoProceed: false,
        allowMultipleUploads: true,
        restrictions: {
            maxFileSize: 400000000,
            maxNumberOfFiles: 10,
            minNumberOfFiles: 1,
            allowedFileTypes: ["image/*"]
        }
    }
)
    .use(Uppy.Webcam)//.use(Uppy.GoogleDrive)
    .use(Uppy.Dashboard, {
        inline: true,
        target: '#drag-drop-area',
        width: '100%',
        height: 400,
        plugins: ['Webcam'],
        metaFields: [
            { id: 'DiaplayOrder', type: 'number', name: 'DiaplayOrder', placeholder: '1', value: '3' },
            { id: 'Title', name: 'Title', placeholder: 'Title' },
            { id: 'name', name: 'Name', placeholder: 'file name' }
        ],
        id: 'Dashboard',
        showProgressDetails: true,
        hideUploadButton: true,
        hideRetryButton: false,
        hidePauseResumeButton: false,
        hideCancelButton: false,
        hideProgressAfterFinish: true,
        closeModalOnClickOutside: true,
        disablePageScrollWhenModalOpen: true,
        animateOpenClose: true,
        proudlyDisplayPoweredByUppy: false,
        showSelectedFiles: true,
        showRemoveButtonAfterComplete: false,
        theme: 'light'
    })
    .use(Uppy.ImageEditor, {
        target: Uppy.Dashboard,
        quality: 0.8
    })
     .use(Uppy.XHRUpload, {
        endpoint: '/Admin/Post/UploadFiles'
    });
     uppy.on('upload-error', function (file, error, response) {
        console.log(file.id);
        console.log(error);
    });


uppy.upload().then((result) => {
    console.info('Successful uploads:', result.successful)

    if (result.failed.length > 0) {
        console.error('Errors:')
        result.failed.forEach((file) => {
            console.error(file.error)
        })
    };
});

uppy.on('upload-success', (file, response) => {
    console.log(file.name, response.uploadURL)
    const img = new Image()
    img.width = 300
    img.alt = file.id
    img.src = response.uploadURL
    document.body.appendChild(img)
});

 