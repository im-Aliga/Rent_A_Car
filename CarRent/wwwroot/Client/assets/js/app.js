            AOS.init();





let Cars = document.querySelector('#Our-car')
let car = document.querySelector('.Our-cars')
Cars.onmouseover=() =>{
    car.style.opacity = 1
  car.style.transform = 'scaleY(1)';
}
  car.onmouseleave=() =>{
    car.style.opacity = 0
    car.style.transform = 'scaleY(0)';
  }


  let car_menu = document.querySelectorAll('.car-category span')
  for(let btn of car_menu){
    btn.onclick=()=>{
      let active = document.querySelector('.Active')
      active.classList.remove('Active')
      btn.classList.add('Active')
    }
  }

  
car_menu.forEach(function(button) {
  button.addEventListener('click', function() {
    var Id = this.getAttribute('id');
    var tabMenus = document.querySelectorAll('.tab-menu');
    tabMenus.forEach(function(menu) {
      if (menu.getAttribute('id') === Id) {
        menu.classList.remove('d-none');
      } else {
        menu.classList.add('d-none');
      }
    });
  });
});

