window.onscroll = function () { scrollFunction() };
function scrollFunction() {
    let imageBrand = document.getElementById("image-brand");
    let main = document.getElementById("main");
    let expandedClassName = "expanded";
    if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50) {
        imageBrand.classList.remove(expandedClassName);
        main.classList.remove(expandedClassName);
    } else {
        imageBrand.classList.add(expandedClassName);
        main.classList.add(expandedClassName);
    }
}