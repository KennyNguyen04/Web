'use strict';

console.log('Script loaded!');

// Lấy phần tử nút bấm và thẻ p thông báo
const btn = document.getElementById('clickMeBtn');
const msg = document.getElementById('message');

// Thêm sự kiện click
btn.addEventListener('click', function () {
    msg.textContent = "You have successfully clicked the button! JS is working.";
    msg.style.color = "green";
    console.log('Button clicked');
});