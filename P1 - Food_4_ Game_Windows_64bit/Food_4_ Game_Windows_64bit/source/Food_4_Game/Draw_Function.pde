
void draw() {



  hs.manage();
  imageMode(0);
  image(bg, 0, 0);
  if (mainMenu == false) {

    image(trash, -200, height-400, 1000, 1000);
  }

  if (GameOver == false) {
    catcher.setLocation(mouseX-100, mouseY-20);
  }
  if (GameOver == true) {
    catcher.setLocation(-1000, -1000);
  } 

  main = new Menu(mainMenu, 1);
  main.display();
  main.click();

  sub1 = new Menu(subMenu1, 2);
  sub1.display2();
  sub1.click2();
  sub2 = new Menu(subMenu2, 3);
  sub2.display3();
  sub2.click3();

  if (muteSound == true) {
    gOsound[0].mute(); 
    for (int i = 0; i<=8; i++) {
      sound[i].mute();
    }
    startup.mute();
    soundSettings.setInt("sound", 0);
    saveTable(settings, "data/settings.csv");
  }
  if (muteSound == false) {
    gOsound[0].unmute();
    for (int i = 0; i<=8; i++) {
      sound[i].unmute();
    }
    startup.unmute();
    soundSettings.setInt("sound", 1);
    saveTable(settings, "data/settings.csv");
  }

  if (muteMusic == true) {
    music.mute();
    soundSettings.setInt("music", 0);
    saveTable(settings, "data/settings.csv");
  }
  if (muteMusic == false) {
    music.unmute();
    soundSettings.setInt("music", 1);
    saveTable(settings, "data/settings.csv");
  }

  if (difficulty > 0.5) {
    difplus = true;
  }
  else {
    difplus = false;
  }

  if (difficulty > 5) {
    difplus2 = true;
  }
  else {
    difplus2 = false;
  }

  if (timer.isFinished() && GameOver == false) {
    if (lessDrops == false || difplus == true) {
      if (evenLessDrops == true || difplus2 == true) {
        drops[totalDrops] = new Drop();
        totalDrops ++ ;
      }
      evenLessDrops = !evenLessDrops;
    }

    lessDrops = !lessDrops;

    if (totalDrops >= drops.length ) {
      totalDrops = 0;
    }
    timer.start();
  }

  for (int i = 0; i < totalDrops; i++ ) {
    drops[i].move();
    drops[i].display(objects);
    if (catcher.intersect(drops[i])) {
      subscorepos1 = drops[i].x+drops[i].size/2;
      subscorepos2 = drops[i].y+drops[i].size/2;

      drops[i].caught();
      score = score+(18-drops[i].type);
      difficulty += 0.1;

      soundNum++;
      if (soundNum == 9) {
        soundNum = 0;
      }
      sound[soundNum].play();
      sound[soundNum].rewind();

      subscore = "+"+(18-drops[i].type);
      subscoretransp = 255;
      subscoresize = 30;
    }
  }

  subscoretransp -= 5;
  subscoresize += 2;
  fill(255, 255, 0, subscoretransp);  
  textSize(subscoresize);

  text(subscore, subscorepos1, subscorepos2);


  for (int i=0; i<totalDrops; i++) {
    if (drops[i].reachedBottom() == true) {
      GameOver = true;
      gOsound[0].play();
    }
  }



  if (restart == true) {
    GameOver = false;
    mainMenu = false;
    restart = false;
    gOsound [0] = minim.loadFile("youlose_sound.wav");
    newHSentry = false;
  }

  if (goToMainMenu == true) {
    GameOver = true;
    mainMenu = true;
    subMenu1 = false;
    subMenu2 = false;
    goToMainMenu = false;
    newHSentry = false;
  }

  if (goToSubMenu1 == true) {
    GameOver = true;
    mainMenu = false;
    subMenu1 = true;
    subMenu2 = false;
    goToSubMenu1 = false;
  }

  if (goToSubMenu2 == true) {
    GameOver = true;
    mainMenu = false;
    subMenu1 = false;
    subMenu2 = true;
    goToSubMenu2 = false;
  }

  if (GameOver == true && mainMenu == false && subMenu1 == false && subMenu2 == false && introduction == false) {
    if (score > 0) {
      finalScore = score;
      difficulty = 0;
    }

    score = 0;
    fill(0);
    fill(255, 0, 0, 255);
    textSize(80);
    text("GAME OVER", 10, height/2);
    fill(255, 150, 0, 255);
    textSize(50);
    text("Final Score: "+finalScore, 20, height/2+50);
    fill(0, 255, 0, 150);
    rect(width/2-90, height/2+80, 180, 70);
    fill(0);
    text("Restart", width/2-85, height/2+130);
    fill(0, 255, 0, 150);
    rect(width/2-90, height/2+150, 180, 70);
    fill(0);
    textSize(30);
    text("Main Menu", width/2-85, height/2+200);
    image(logo, 0, 0, 500, 250);
    if (newHSentry == true) {
      textSize(20);
      fill(0);
      text("You have made it into the Highscores!", 50, height/2-120, 20);
    }
  }









  if (mousePressed && mouseButton == LEFT && mouseX > 150 && mouseX < 330 && mouseY > 480 && mouseY < 550 && mainMenu == false && GameOver == true && subMenu1 == false && subMenu2 == false && introduction == false) {
    restart = !restart;
    startup = minim.loadFile("startup.wav");
    startup.play();
    timer.start();
  }

  if (mousePressed && mouseButton == LEFT && mouseX > 150 && mouseX < 330 && mouseY > 550 && mouseY < 620 && GameOver == true && mainMenu == false && subMenu1 == false && subMenu2 == false && introduction == false) {
    startup = minim.loadFile("startup.wav");
    goToMainMenu = !goToMainMenu;
    timer2.start();
  }

  if (GameOver == true && mainMenu == true && restart == true) {
    goToMainMenu = !goToMainMenu;
  }



  if (mainMenu == false && GameOver == false && subMenu1 == false && subMenu2 == false) {
    fill(0, 255, 0, 150);
    textSize(100);
    text(score, CENTER, 100);
  }

  if (GameOver == false && mainMenu == false) {
    rect(400, 20, 70, 70);
    fill(0);
    textSize(16);
    text("MENU", 410, 60);
  }

  if (mousePressed && mouseButton == LEFT && mouseX > 400 && mouseX < 470 && mouseY > 20 && mouseY < 90 && GameOver == false && mainMenu == false && subMenu1 == false && subMenu2 == false) {
    GameOver = true;
    goToMainMenu = !goToMainMenu;
    timer2.start();
    for (int i = 0; i < totalDrops; i++ ) {
      drops[i].caught();
    }
  }

  if (mainMenu == false && subMenu1 == false && subMenu2 == false) {

    image(trash2, -200, height-400, 1000, 1000);
  }  

  if (mainMenu == false && GameOver == false && mousePressed == false && subMenu1 == false && subMenu2 == false) {
    noCursor();
    imageMode(CENTER);
    image(cursor, mouseX, mouseY, 300, 150);
  }
  else if (mainMenu == false && GameOver == false && mousePressed == true && subMenu1 == false &&subMenu2 == false) {
    noCursor();
    imageMode(CENTER);
    image(cursor, mouseX, mouseY, 200, 100);
  }
  else {
    cursor();
  }

  if (introduction == true) {
    mainMenu = false;
    if (mousePressed == true && timer2.isFinished()) {
      slide++;
      timer2.start();
    } 
    if (slide >= introductionSlides.length) {
      introduction = false;
      mainMenu = true;
      settings.setInt(0, "introduction", 0);
      saveTable(settings, "data/settings.csv");
    }
    else {

      image(introductionSlides[slide], 0, 0, 480, 800);
    }
  }
}

