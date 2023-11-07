var myCollapsible = document.getElementById('collapseFilter');
document.getElementById('select-status').style.overflow = 'hidden';
myCollapsible.addEventListener('hidden.bs.collapse', function () {
    document.getElementById('select-status').style.overflow = 'hidden';
});
myCollapsible.addEventListener('shown.bs.collapse', function () {
    document.getElementById('select-status').style.overflow = 'visible';
})