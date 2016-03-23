import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import ddf.minim.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class Food_4_Game extends PApplet {

// Food 4 Game
// Group 113
// Medialogy, Semester 1, 2013
// Aalborg University Copenhagen
class Catcher {
  float r;
  float x, y;

  Catcher(float tempR) {
    r = tempR;
    x = 0;
    y = 0;
  }

  public void setLocation(float tempX, float tempY) {
    x = tempX;
    y = tempY;
  }

  public void display() {
  }

  public boolean intersect(Drop d) {
    float distance = dist(x, y, d.x, d.y); 
    if (distance < r + d.r  && mouseButton == LEFT) { 
      return true;
    } 
    else {
      return false;
    }
  }
}

public int difficultyInc() {
  int retValue =0;
  if (difficulty > 7 && difficulty < 9) {
    retValue =  1;
  }
  else if (difficulty > 9 && difficulty < 10) {
    retValue =  2;
  }
  else if (difficulty > 10 && difficulty < 11) {
    retValue =  3;
  }
  else if (difficulty > 11 && difficulty < 12) {
    retValue =  4;
  }
  else if (difficulty > 12 && difficulty < 13) {
    retValue =  5;
  }
  else if (difficulty > 13 && difficulty < 14) {
    retValue =  6;
  }
  else if (difficulty > 14 && difficulty < 15) {
    retValue =  7;
  }
  else if (difficulty > 15) {
    retValue =  PApplet.parseInt(difficulty/2);
  }
  return retValue;
}


public void draw() {



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

  if (difficulty > 0.5f) {
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
      difficulty += 0.1f;

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

class Drop {

  float x, y;   
  float speed; 
  float r;     
  float size;
  int type;
  PShape objects;

  Drop() {
    r = 30;                 
    x = random(380);     
    y = -r*4;              
    type = type();
    speed = (18-type+difficultyInc())/2;   
    size = 250-random(100);
  }

  public void move() {
    y += speed;
  }

  public boolean reachedBottom() {
    if (y > height + r*4) { 
      for (int i=0; i<totalDrops; i++) {
        drops[i].caught();
      }
      return true;
    } 
    else {
      return false;
    }
  }

  public void display(PImage [] objects) {

    image(objects[type], x, y, size, size);
  }

  public void caught() {
    speed = 0; 
    y = - 1000;
  }
}



Catcher catcher;    
Timer timer;        
Timer timer2;
Drop[] drops;       
Drop[] drops2; 
Menu main;
Menu sub1;
Menu sub2;
AudioPlayer [] sound = new AudioPlayer[9];
AudioPlayer [] gOsound = new AudioPlayer[1];
AudioPlayer startup;
AudioPlayer music;
Minim minim;
Minim minim2;

int score = 0;
int finalScore;
int soundNum = 0;
int totalDrops = 0;
int slide = 0;

boolean GameOver;
boolean restart;
boolean lessDrops = false;
boolean evenLessDrops = false;
boolean difplus = false;
boolean difplus2 = false;
boolean mainMenu=true;
boolean goToMainMenu;
boolean subMenu1;
boolean subMenu2;
boolean goToSubMenu1;
boolean goToSubMenu2;
boolean muteSound;
boolean muteMusic;
boolean introduction;
boolean newHSentry = false;

PImage bg;
PImage [] objects = new PImage[17];
PImage trash;
PImage trash2;
PImage cursor;
PImage logo;
PImage soundmute;
PImage musicmute;
PImage [] introductionSlides = new PImage[6];


float difficulty;
float subscorepos1 = -1000;
float subscorepos2 = -100;
float subscoretransp = 0;
float subscoresize = 30;

Table highscores;
Table settings;
TableRow pos0, pos1, pos2, pos3, pos4, pos5; 
highscore hs;
TableRow soundSettings;

String subscore = "0";

class highscore {

  highscore() {
  }
  int temp0, temp1, temp2, temp3, temp4, temp5;

  public void manage() {
    if (finalScore > pos4.getInt(1) && finalScore < pos3.getInt(1)) {
      highscores.setInt(4, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
    else if (finalScore > pos3.getInt(1) && finalScore < pos2.getInt(1)) {
      temp3 = pos3.getInt(1);
      pos4.setInt(1, temp3) ;
      highscores.setInt(3, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
    else if (finalScore > pos2.getInt(1) && finalScore < pos1.getInt(1)) {
      temp3 = pos3.getInt(1);
      pos4.setInt(1, temp3) ;
      temp2 = pos2.getInt(1);
      pos3.setInt(1, temp2);
      highscores.setInt(2, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
    else if (finalScore > pos1.getInt(1) && finalScore < pos0.getInt(1)) {
      temp3 = pos3.getInt(1);
      pos4.setInt(1, temp3) ;
      temp2 = pos2.getInt(1);
      pos3.setInt(1, temp2);
      temp1 = pos1.getInt(1);
      pos2.setInt(1, temp1);
      highscores.setInt(1, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
    else if (finalScore > pos0.getInt(1)) {
      temp3 = pos3.getInt(1);
      pos4.setInt(1, temp3) ;
      temp2 = pos2.getInt(1);
      pos3.setInt(1, temp2);
      temp1 = pos1.getInt(1);
      pos2.setInt(1, temp1);
      temp0 = pos0.getInt(1);
      pos1.setInt(1, temp0);
      highscores.setInt(0, "score", finalScore);
      saveTable(highscores, "data/highscores.csv");
      newHSentry = true;
    }
  }
}

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

  public void display() {

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

  public void display2() {
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


  public void display3() {
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

  public void click2() {
    if (mousePressed && mouseButton == LEFT && mouseX > width/2-textBoxSize1/2 && mouseX < width/2-textBoxSize1/2+textBoxSize1 && mouseY > height-250 && mouseY < height-250+textBoxSize2 && subMenu1 == true) {
      subMenu1 =false;
      goToMainMenu = true;
    }
  }

  public void click3() {
    if (mousePressed && mouseButton == LEFT && mouseX > width/2-textBoxSize1/2 && mouseX < width/2-textBoxSize1/2+textBoxSize1 && mouseY > height-250 && mouseY < height-250+textBoxSize2 && subMenu2 == true) {
      subMenu2 =false;
      goToMainMenu = true;
    }
  }

  public void click() {
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

public void setup() {
  size(480, 800);
  smooth();

  objects [0] = loadImage("Fish.png");
  objects [1] = loadImage("Milk.png");
  objects [2] = loadImage("Banana.png");
  objects [3] = loadImage("Cabbage.png");
  objects [4] = loadImage("Tomato.png");
  objects [5] = loadImage("Chicken.png");
  objects [6] = loadImage("Apple.png");
  objects [7] = loadImage("Rettich.png");
  objects [8] = loadImage("Potato.png");
  objects [9] = loadImage("Egg.png");
  objects [10] = loadImage("Steak.png");
  objects [11] = loadImage("Bread.png");
  objects [12] = loadImage("Carrot.png");
  objects [13] = loadImage("Cheese.png");
  objects [14] = loadImage("Jam.png");
  objects [15] = loadImage("Candy.png");
  objects [16] = loadImage("Mushroom.png");

  minim = new Minim(this);

  sound [0] = minim.loadFile("Note1.wav");
  sound [1] = minim.loadFile("Note2.wav");
  sound [2] = minim.loadFile("Note3.wav");
  sound [3] = minim.loadFile("Note4.wav");
  sound [4] = minim.loadFile("Note5.wav");
  sound [5] = minim.loadFile("Note6.wav");
  sound [6] = minim.loadFile("Note7.wav");
  sound [7] = minim.loadFile("Note8.wav");
  sound [8] = minim.loadFile("Note9.wav");

  gOsound [0] = minim.loadFile("youlose_sound.wav");

  startup = minim.loadFile("startup.wav");

  music = minim.loadFile("intro.wav");

  catcher = new Catcher(60); 
  drops = new Drop[1000];    
  drops2 = new Drop[10];
  timer = new Timer(300);   
  timer.start();             
  timer2 = new Timer(150);

  GameOver = true;
  restart = false;

  bg = loadImage("Game_BGround.png");
  trash = loadImage("Trashcan.png");
  trash2 = loadImage("Trashcan2.png");
  logo = loadImage("Logo.png");
  cursor = loadImage("cursor.png");
  soundmute = loadImage("soundmute.png");
  musicmute = loadImage("musicmute.png");

  music.loop();
  music.mute();

  highscores = new Table();

  highscores = loadTable("highscores.csv", "header"); 

  for (TableRow row : highscores.rows()) {
    int place = row.getInt("place");
    int score = row.getInt("score");
    String name = row.getString("name");
  }
  pos0 = highscores.getRow(0);
  pos1 = highscores.getRow(1);
  pos2 = highscores.getRow(2);
  pos3 = highscores.getRow(3);
  pos4 = highscores.getRow(4);
  pos5 = highscores.getRow(5);

  hs = new highscore();

  settings = new Table();

  settings = loadTable("settings.csv", "header"); 

  for (TableRow row : settings.rows()) {
    int sound = row.getInt("sound");
    int music = row.getInt("music");
    int introduction = row.getInt("introduction");
  }

  soundSettings = settings.getRow(0);

  if (soundSettings.getInt("sound") == 0) {
    muteSound = true;
  }
  else if (soundSettings.getInt("sound") == 1) {
    muteSound = false;
  }

  if (soundSettings.getInt("music") == 0) {
    muteMusic = true;
  }
  else if (soundSettings.getInt("music") == 1) {
    muteMusic = false;
  }

  if (soundSettings.getInt("introduction") == 1) {
    introduction = true;
  }
  else if (soundSettings.getInt("introduction") == 0) {
    introduction = false;
  }

  introductionSlides[0] = loadImage("intro1.jpg");
  introductionSlides[1] = loadImage("intro2.jpg");
  introductionSlides[2] = loadImage("intro3.jpg");
  introductionSlides[3] = loadImage("intro4.jpg");
  introductionSlides[4] = loadImage("intro5.jpg");
  introductionSlides[5] = loadImage("intro6.jpg");
}

class Timer {

  int savedTime;
  int totalTime;

  Timer(int tempTotalTime) {
    totalTime = tempTotalTime;
  }

  public void start() {
    savedTime = millis();
  }

  public boolean isFinished() { 
    int passedTime = millis()- savedTime;
    if (passedTime > totalTime) {
      return true;
    } 
    else {
      return false;
    }
  }
}

public int type() {
  int ret=16;
  if (difficulty<1) {
    ret=PApplet.parseInt(random(3))+14;
  }
  else if (difficulty>1 && difficulty<2) {
    ret= (PApplet.parseInt(random(5))+12);
  }
  else if (difficulty>2 && difficulty<3) {
    ret= (PApplet.parseInt(random(8))+9);
  }
  else  if (difficulty>3 && difficulty<4) {
    ret = (PApplet.parseInt(random(13))+4);
  }
  else if (difficulty>4 && difficulty < 6) {
    ret = (PApplet.parseInt(random(17)));
  }
  else if (difficulty>6 && difficulty<7) {
    float difchange = random(5);
    if (difchange > 2) {
      ret = PApplet.parseInt(random(13));
    }
    else {
      ret = (PApplet.parseInt(random(17)));
    }
  }
  else if (difficulty>7 && difficulty<8) {
    float difchange = random(5);
    if (difchange > 2) {
      ret = PApplet.parseInt(random(8));
    }
    else {
      ret = (PApplet.parseInt(random(17)));
    }
  }
  else if (difficulty>8 && difficulty<9) {
    float difchange = random(5);
    if (difchange > 1) {
      ret = PApplet.parseInt(random(5));
    }
    else {
      ret = (PApplet.parseInt(random(17)));
    }
  }
  else if (difficulty>9) {
    float difchange = random(5);
    if (difchange > 2) {
      ret = PApplet.parseInt(random(3));
    }
    else {
      ret = (PApplet.parseInt(random(17)));
    }
  } 
  return ret;
}

  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "--full-screen", "--bgcolor=#666666", "--hide-stop", "Food_4_Game" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
