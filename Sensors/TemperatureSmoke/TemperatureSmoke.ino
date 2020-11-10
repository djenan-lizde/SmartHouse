#include "DHT.h"
#define DHTTYPE DHT11
#include <MQ2.h>

//change this with the pin that you use
int pin = A0;
int lpg, co, smoke;

DHT dht(2,DHTTYPE);

void setup()
{
  Serial.begin(9600);
  Serial.println("TEST");
  dht.begin();
  mq2.begin();
}

void loop(){
   float h = dht.readHumidity();
   float t = dht.readTemperature();

   Serial.println("Current Humidity =");
   Serial.println(h);

   Serial.println("Current temperature =");
   Serial.println(t);

   float* values= mq2.read(true);
  
  //lpg = values[0];
  lpg = mq2.readLPG();
  //co = values[1];
  co = mq2.readCO();
  //smoke = values[2];
  smoke = mq2.readSmoke();

   delay(2000);
}
