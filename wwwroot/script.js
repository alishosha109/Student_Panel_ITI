

// check all checkboxes when the user checks the main checkbox
function checkUncheck(checkBox) {
  get = document.getElementsByName("data");
  for (var i = 0; i < get.length; i++) {
    get[i].checked = checkBox.checked;
  }
}

// handles the three dots (details)
$(function () {
  $('[data-toggle="popover"]').popover({
    html: true,
    sanitize: false,
    content: function () {
      var content = $(this).attr("data-content");
      return '<div class="popover-content">' + content + "</div>";
    },
  });
});
const tooltipTriggerList = document.querySelectorAll(
  '[data-bs-toggle="tooltip"]'
);
const tooltipList = [...tooltipTriggerList].map(
  (tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl)
);

// sort the table by each row
function sortTable(columnIndex) {
  var table, rows, switching, i, x, y, shouldSwitch;
  table = document.getElementById("myTable");
  switching = true;
  while (switching) {
    switching = false;
    rows = table.rows;
    for (i = 1; i < rows.length - 1; i++) {
      shouldSwitch = false;
      x = rows[i].getElementsByTagName("td")[columnIndex];
      y = rows[i + 1].getElementsByTagName("td")[columnIndex];
      // Check if the values are numbers
      var isNum = !isNaN(parseFloat(x.innerHTML)) && isFinite(x.innerHTML);
      if (isNum) {
        if (parseFloat(x.innerHTML) > parseFloat(y.innerHTML)) {
          shouldSwitch = true;
          break;
        }
      } else {
        if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
          shouldSwitch = true;
          break;
        }
      }
    }
    if (shouldSwitch) {
      rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
      switching = true;
    }
  }
}

function bubbleSort(arr) {
  var len = arr.length;
  var swapped;
  do {
    swapped = false;
    for (var i = 0; i < len - 1; i++) {
      if (arr[i] > arr[i + 1]) {
        var temp = arr[i];
        arr[i] = arr[i + 1];
        arr[i + 1] = temp;
        swapped = true;
      }
    }
  } while (swapped);
  return arr;
}

// handles the black screen and the delete screen appearance
const showScreensBtn = document.getElementById("show-screens-btn");
const overlay = document.querySelector(".overlay");
const floatingScreen = document.getElementById("floating-screen");

if (showScreensBtn != null) showScreensBtn.addEventListener("click", clicked);

function clicked() {
  overlay.classList.add("show");
  floatingScreen.classList.add("show");
}




