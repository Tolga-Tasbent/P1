void setup() {
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

