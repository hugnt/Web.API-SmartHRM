(document.querySelectorAll("[toast-list]") ||
  document.querySelectorAll("[data-choices]") ||
  document.querySelectorAll("[data-provider]")) &&
  (document.writeln(
    "<script type='text/javascript' src='https://cdn.jsdelivr.net/npm/toastify-js'></script>"
  ),
  document.writeln(
    "<script type='text/javascript' src='https://localhost:7186/assets/libs/choices.js/public/assets/scripts/choices.min.js'></script>"
  ),
  document.writeln(
    "<script type='text/javascript' src='https://localhost:7186/assets/libs/flatpickr/flatpickr.min.js'></script>"
  ));
