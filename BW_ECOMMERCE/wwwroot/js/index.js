window.addEventListener('scroll', function () {
    var fruttaSection = document.getElementById('frutta');

    // Verifica la posizione dello scroll
    if (window.scrollY + window.innerHeight > fruttaSection.offsetTop) {
        // Aggiungi la classe "scrolled" quando la sezione "frutta" è visibile
        fruttaSection.classList.add('scrolled');
    } else {
        // Rimuovi la classe "scrolled" quando la sezione "frutta" non è più visibile
        fruttaSection.classList.remove('scrolled');
    }
});
window.addEventListener('scroll', function () {
    var verduraSection = document.getElementById('verdura');

    // Verifica la posizione dello scroll
    if (window.scrollY + window.innerHeight > verduraSection.offsetTop) {
        // Aggiungi la classe "scrolled" quando la sezione "frutta" è visibile
        verduraSection.classList.add('scrolled');
    } else {
        // Rimuovi la classe "scrolled" quando la sezione "frutta" non è più visibile
        verduraSection.classList.remove('scrolled');
    }
});
