// When the user scrolls down 50px from the top of the document, resize the header's font size
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    let headerBrand = document.getElementById("header-brand");
    if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50) {
        headerBrand.classList.replace("col-4", "col-3");
    } else {
        headerBrand.classList.replace("col-3", "col-4");
    }
}