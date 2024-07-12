// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.addEventListener('scroll', function () {
    var gusta = document.getElementById('gusta');

    // Verifica la posizione dello scroll
    if (window.scrollY > 650) {
        // Aggiungi la classe "scrolled" quando la pagina è scrollata più di 50px
        gusta.classList.remove('myGreen');
        gusta.classList.add('myOrange');
    } else {
        // Rimuovi la classe "scrolled" quando lo scroll è meno di 50px
        gusta.classList.remove('myOrange');
        gusta.classList.add('myGreen');

    }
});
