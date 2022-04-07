var hamburger = document.getElementById('hamburger');
var navContent = document.getElementById('navContent');

hamburger.addEventListener("click", OnClickHamburger);

function OnClickHamburger(){
  navContent.classList.toggle("show");
}
