class Menu {

  boolean _d;
  int textBoxSize1;
  int textBoxSize2;
  int _type;
  float x;
  float y;


  Menu(boolean d, int type) {

    _d=d;
    _type = type;
    textBoxSize1 = 180;
    textBoxSize2 = 70;
    x = width/2;
    y = height/2-100;
  }

  void display() {

    if (_d == true && _type == 1) {
      fill(255);
      rect(0, 0, width, height);
      fill(0, 255, 0, 150);
      rect(x-textBoxSize1/2, y-textBoxSize2/2, textBoxSize1, textBoxSize2);
      fill(0);
      textSize(30);
      text("Game", x-textBoxSize1/2+50, y+5);
      fill(0, 255, 0, 150);
      rect(x-textBoxSize1/2, y-textBoxSize2/2+textBoxSize2, textBoxSize1, textBoxSize2);
      fill(0);
      textSize(30);
      text("High Scores", x-textBoxSize1/2+5, y+5+textBoxSize2);
      fill(0, 255, 0, 150);
      rect(x-textBoxSize1/2, y-textBoxSize2/2+2*textBoxSize2, textBoxSize1, textBoxSize2);
      fill(0);
      textSize(30);
      text("About", x-textBoxSize1/2+50, y+5+2*textBoxSize2);
      fill(0, 255, 0, 150);
      rect(x-textBoxSize1/2, y-textBoxSize2/2+3*textBoxSize2, textBoxSize1, textBoxSize2);  
      fill(0);
      textSize(30);
      text("Exit", x-textBoxSize1/2+60, y+5+3*textBoxSize2);
      image(logo, 0, 0, 500, 250);
      fill(0, 255, 0, 150);
      image(soundmute, x-textBoxSize1/2+20, y-textBoxSize2/2+4*textBoxSize2+50, textBoxSize1/3, textBoxSize2);
      image(musicmute, x+20, y-textBoxSize2/2+4*textBoxSize2+50, textBoxSize1/3, textBoxSize2);
      if (muteSound==false) {
        fill(0, 255, 0, 150);
        stroke(0);
        rect(x-textBoxSize1/2, y-textBoxSize2/2+4*textBoxSize2+50, textBoxSize1/2, textBoxSize2);
        stroke(255);
      }
      else if (muteSound == true) {
        stroke(0);
        noFill();
        rect(x-textBoxSize1/2, y-textBoxSize2/2+4*textBoxSize2+50, textBoxSize1/2, textBoxSize2);
        stroke(255);
      }
      if (muteMusic==false) {
        stroke(0);
        fill(0, 255, 0, 150);
        rect(x, y-textBoxSize2/2+4*textBoxSize2+50, textBoxSize1/2, textBoxSize2);
        stroke(255) ;
      }
      else if (muteMusic == true) {
        stroke(0);
        noFill();
        rect(x, y-textBoxSize2/2+4*textBoxSize2+50, textBoxSize1/2, textBoxSize2);
        stroke(255) ;
      }
      textSize(12);
      fill(0);
      text("GROUP 113, Aalborg University Copenhagen 2013", 100, height-100) ;
    }
  }

  void display2() {
    if (_d == true && _type == 2) {
      fill(255);
      rect(0, 0, width, height);
      fill(255, 255, 0, 150);
      rect(50, 50, width-100, height-200);
      fill(0);
      textSize(50);
      text("HIGH SCORES", width/2-160, 120);

      textSize(20);
      text("1. "+highscores.getString(0, "name")+" - "+highscores.getInt(0, "score"), 80, 200); 
      text("2. "+highscores.getString(1, "name")+" - "+highscores.getInt(1, "score"), 80, 250); 
      text("3. "+highscores.getString(2, "name")+" - "+highscores.getInt(2, "score"), 80, 300); 
      text("4. "+highscores.getString(3, "name")+" - "+highscores.getInt(3, "score"), 80, 350); 
      text("5. "+highscores.getString(4, "name")+" - "+highscores.getInt(4, "score"), 80, 400); 

      fill(255, 255, 255, 150);
      rect(width/2-textBoxSize1/2, height-250, textBoxSize1, textBoxSize2);
      fill(0);
      textSize(40);
      text("Back", width/2-50, height-200);


      fill(0);
      textSize(12);
      text("GROUP 113, Aalborg University Copenhagen 2013", 90, height-100);
    }
  }


  void display3() {
    if (_d == true && _type == 3) {
      fill(255);
      rect(0, 0, width, height);
      image(logo, 0, height-500, 500, 250);
      fill(0, 150, 255, 150);
      rect(50, 50, width-85, height-200);
      fill(0);
      textSize(50);
      //text("Food 4 Game", width/2-160, 120);
      fill(255, 255, 255, 150);
      rect(width/2-textBoxSize1/2, height-250, textBoxSize1, textBoxSize2);
      fill(0);
      textSize(40);
      text("Back", width/2-50, height-200);
      fill(0);
      textSize(18);
      text("Each year we  waste  about 1.6 million tons ", 60, 100); 
      text("of  food  which  equals  $750 billion.  Food ", 60, 130); 
      text("waste  is  responsible for  28%  of  the  gas", 60, 160);
      text("emission and 17% of the greenhouse effect.", 60, 190);
      text("This game is not about  winning, but about ", 60, 220);
      text("making   people   aware  of  what they  are", 60, 250);
      text("throwing out. This  game  was  created  by ", 60, 280);
      text("university students and it is in  a prototype", 60, 310);
      text("state.", 60, 340);
      fill(0);

      textSize(12);
      text("GROUP 113, Aalborg University Copenhagen 2013", 90, height-100);
    }
  }

  void click2() {
    if (mousePressed && mouseButton == LEFT && mouseX > width/2-textBoxSize1/2 && mouseX < width/2-textBoxSize1/2+textBoxSize1 && mouseY > height-250 && mouseY < height-250+textBoxSize2 && subMenu1 == true) {
      subMenu1 =false;
      goToMainMenu = true;
    }
  }

  void click3() {
    if (mousePressed && mouseButton == LEFT && mouseX > width/2-textBoxSize1/2 && mouseX < width/2-textBoxSize1/2+textBoxSize1 && mouseY > height-250 && mouseY < height-250+textBoxSize2 && subMenu2 == true) {
      subMenu2 =false;
      goToMainMenu = true;
    }
  }

  void click() {
    if (timer2.isFinished()) {
      if (mousePressed && mouseButton == LEFT && mouseX > x-textBoxSize1/2 && mouseX < x-textBoxSize1/2+textBoxSize1 && mouseY > y-textBoxSize2/2 && mouseY < y-textBoxSize2/2+textBoxSize2 && mainMenu == true) {
        restart = !restart;
        startup = minim.loadFile("startup.wav");
        startup.play();
        timer.start();
      }

      if (mousePressed && mouseButton == LEFT && mouseX > x-textBoxSize1/2 && mouseX < x-textBoxSize1/2+textBoxSize1 && mouseY > y-textBoxSize2/2+textBoxSize2 && mouseY < y-textBoxSize2/2+textBoxSize2*2 && mainMenu == true) {
        goToSubMenu1=true;
      }

      if (mousePressed && mouseButton == LEFT && mouseX > x-textBoxSize1/2 && mouseX < x-textBoxSize1/2+textBoxSize1 && mouseY > y-textBoxSize2/2+textBoxSize2*2 && mouseY < y-textBoxSize2/2+textBoxSize2*3 && mainMenu == true) {
        goToSubMenu2=true;
      }

      if (mousePressed && mouseButton == LEFT && mouseX > x-textBoxSize1/2 && mouseX < x-textBoxSize1/2+textBoxSize1 && mouseY > y-textBoxSize2/2+3*textBoxSize2 && mouseY < y-textBoxSize2/2+3*textBoxSize2+textBoxSize2 && mainMenu == true) {
        exit();
      }

      if (timer2.isFinished() && mousePressed && mouseButton == LEFT && mouseX > x-textBoxSize1/2 && mouseX < x-textBoxSize1/2+textBoxSize1/2 && mouseY > y-textBoxSize2/2+4*textBoxSize2+50 && mouseY < y-textBoxSize2/2+4*textBoxSize2+50+textBoxSize2 && mainMenu == true) {
        muteSound = !muteSound;
        timer2.start();
      }

      if (timer2.isFinished() && mousePressed && mouseButton == LEFT && mouseX > x && mouseX < x+textBoxSize1/2 && mouseY > y-textBoxSize2/2+4*textBoxSize2+50 && mouseY < y-textBoxSize2/2+4*textBoxSize2+50+textBoxSize2 && mainMenu == true) {
        muteMusic = !muteMusic;
        timer2.start();
      }
    }
  }
}

