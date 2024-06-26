window.addEventListener('load', function () {
    const fileInput = document.getElementById('file-input');
    const submitButton = document.getElementById('file-submit');
    const importSuccessMessage = document.getElementById('importSuccessMessage');
    const importFailMessage = document.getElementById('importFailMessage');

    let selectedFile = null;

    fileInput.addEventListener('change', (e) => {
        selectedFile = fileInput.files[0];
        console.log(`Selected file: ${selectedFile.name}`);
    });

    submitButton.addEventListener('click', (e) => {
        e.preventDefault();
        if (selectedFile) {
            const formData = new FormData();
            formData.append('file', selectedFile);

            fetch('/ImportFile/ImportJson', {
                method: 'POST',
                body: formData
            })
                .then((response) => response.json())
                .then((data) => {
                    if (data.success) {
                        importSuccessMessage.style.display = 'block';
                        importFailMessage.style.display = 'none';
                    }
                    else {
                        importSuccessMessage.style.display = 'none';
                        importFailMessage.style.display = 'block';
                    }
                    console.log(data)
                })
                .catch((error) => console.error(error));
        } else {
            alert('Please select a file first!');
        }
    });
});

