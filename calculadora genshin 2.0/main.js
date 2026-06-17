fetch("navbar.html")
      .then(res => res.text())
      .then(html => {
        document.getElementById("navbar-container").innerHTML = html;

        // Resalta el enlace activo según la página actual
        const links = document.querySelectorAll(".nav-links a");
        links.forEach(link => {
          if (link.href === window.location.href) {
            link.classList.add("active");
          }
        });
      });
 
function toggleSeccion(){
    const seccion = Document.getElementById("Calculadora_2");
    seccion.classList.toggleSeccion("visible")
}