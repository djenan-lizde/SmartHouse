#include "DHT.h"
#include "ArduinoJson.h"
#include <Servo.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <WiFiClientSecure.h>

#define DHTPIN 14
#define DHTTYPE DHT11   // DHT 11
#define MQ2pin 34

int servoPin = 26;
int pos = 0, humidity, heatIndex, temperatureFahrenheit, temperatureCelsius, tempConfig;  // variable to store the servo position
bool isWindowOpen = false;

const char* ssid     = "Trio Fantastico";
const char* password = "thisisapassword2019$";

//const char* ssid = "bd3996";
//const char* password = "286466916";

DHT dht(DHTPIN, DHTTYPE);
Servo servo1;
HTTPClient httpsClient;

void setup() {
  Serial.begin(115200);
  // We start by connecting to a WiFi network

  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  servo1.attach(servoPin);
  servo1.write(0);
  dht.begin();
  delay(20000);
}

void loop() {
  // Wait a few seconds between measurements.
  delay(5000);

  if (WiFi.status() == WL_CONNECTED) {
    httpsClient.begin("https://smarthouseapi20210508183300.azurewebsites.net/api/configuration/current");
    httpsClient.GET();
    String payload = httpsClient.getString();
    StaticJsonDocument<200> jsonDoc;
    DeserializationError error = deserializeJson(jsonDoc, payload);
    tempConfig = jsonDoc["temperatureCelsius"];
  }

  temperature();
  gas();
}

void openWindow() {
  for (int posDegrees = 0; posDegrees <= 90; posDegrees++) {
    servo1.write(posDegrees);
    delay(20);
  }
}

void closeWindow() {
  for (int posDegrees = 90; posDegrees >= 0; posDegrees--) {
    servo1.write(posDegrees);
    delay(20);
  }
}

void gas() {
  int gassensorAnalog = analogRead(MQ2pin);

  if (gassensorAnalog >= 1000 && isWindowOpen == false) {
    openWindow();
    if (WiFi.status() == WL_CONNECTED) {
      httpsClient.begin("https://smarthouseapi20210508183300.azurewebsites.net/api/sms/sendsms");
      httpsClient.GET();
      isWindowOpen = true;
    }
    Serial.println("Message sent!!!!");
    Serial.print("Value: ");
    Serial.println(gassensorAnalog);
  }

  if (gassensorAnalog < 1000 && isWindowOpen == true) {
    closeWindow();
    isWindowOpen = false;
    Serial.println(gassensorAnalog);
  }
}

void temperature() {
  // Reading temperature or humidity takes about 250 milliseconds!
  // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)
  humidity = dht.readHumidity();
  temperatureCelsius = dht.readTemperature();
  temperatureFahrenheit = dht.readTemperature(true);
  heatIndex = dht.computeHeatIndex(temperatureCelsius, humidity, false);

  // Check if any reads failed and exit early (to try again).
  if (isnan(humidity) || isnan(temperatureCelsius) || isnan(temperatureFahrenheit) || isnan(heatIndex)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  if (temperatureCelsius >= tempConfig && isWindowOpen == false) {
    openWindow();
    isWindowOpen = true;
  }

  if (temperatureCelsius < tempConfig && isWindowOpen == true) {
    closeWindow();
    isWindowOpen = false;
  }

  StaticJsonDocument<1024> doc;
  doc["temperatureCelsius"] = temperatureCelsius;
  doc["temperatureFahrenheit"] = temperatureFahrenheit;
  doc["humidity"] = humidity;
  doc["heatIndex"] = heatIndex;

  serializeJsonPretty(doc, Serial);

  if (WiFi.status() == WL_CONNECTED) {
    httpsClient.begin("https://smarthouseapi20210508183300.azurewebsites.net/api/temperatures/" + String(temperatureCelsius) + "/" + String(temperatureFahrenheit) + "/" + String(humidity) + "/" + String(heatIndex));
    httpsClient.GET();
  }
}
