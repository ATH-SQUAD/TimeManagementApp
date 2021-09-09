let cardElement = document.querySelector(".card");

cardElement.addEventListener("click", flip);

function flip() {
  cardElement.classList.toggle("flipped")
}

function startTime() {
  var weekday = new Array();
  weekday[0] = "Niedziela";
  weekday[1] = "Poniedziałek";
  weekday[2] = "Wtorek";
  weekday[3] = "Środa";
  weekday[4] = "Czwartek";
  weekday[5] = "Piątek";
  weekday[6] = "Sobota";
  var month = new Array();
  month[0] = "Styczeń";
  month[1] = "Luty";
  month[2] = "Marzec";
  month[3] = "Kwiecień";
  month[4] = "Maj";
  month[5] = "Czerwiec";
  month[6] = "Lipiec";
  month[7] = "Sierpień";
  month[8] = "Wrzesień";
  month[9] = "Październik";
  month[10] = "Listopad";
  month[11] = "Grudzień";
  var today = new Date();
  var h = today.getHours();
  var m = today.getMinutes();
  var s = today.getSeconds();
  var d = today.getDate();
  var y = today.getFullYear();
  var wd = weekday[today.getDay()];
  var mt = month[today.getMonth()];

  m = checkTime(m);
  s = checkTime(s);
  document.getElementById('date').innerHTML =
    d;
  document.getElementById('day').innerHTML =
    wd;
  document.getElementById('month').innerHTML =
    mt + "/" + y;

  var t = setTimeout(startTime, 500);
}
function checkTime(i) {
  if (i < 10) { i = "0" + i };
  return i;
}
