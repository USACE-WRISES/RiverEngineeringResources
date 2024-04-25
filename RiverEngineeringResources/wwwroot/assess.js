﻿let dotnetHelper = null;

export function load() {
    return "";
}

export function initializeDotnetHelper(dotnetReference) {
    dotnetHelper = dotnetReference;
}

// Intended to be called via IJSInterop to prompt the download of a file generated by the Blazor app.
export function promptDownload(fileName, fileBytesBase64) {
    // Create a hidden <a href=""></a> where the "href" attribute contains the contents of the file in Base-64 form.
    var hyperlink = document.createElement("a");
    hyperlink.download = fileName;
    hyperlink.href = "data:application/octet-stream;base64," + fileBytesBase64;
    hyperlink.style = "display:none;";

    // Add the link tag to the end of the DOM..
    document.body.appendChild(hyperlink);

    // Simulate a click event to prompt the download.
    hyperlink.click();

    // Remove the link from the DOM.
    document.body.removeChild(hyperlink);
}

export function openFileNewTab(filePath) {
    const anchor = document.createElement('a');
    anchor.href = filePath;
    anchor.target = '_blank'; // Ensures it opens in a new tab
    anchor.style.display = 'none';
    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);
}

export function downloadFile(filePath, fileName) {
    const anchor = document.createElement('a');
    anchor.href = filePath;
    anchor.download = fileName;
    //alert("Hi");
    //anchor.target = '_blank'; // Ensures it opens in a new tab
    anchor.style.display = 'none';
    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);
}