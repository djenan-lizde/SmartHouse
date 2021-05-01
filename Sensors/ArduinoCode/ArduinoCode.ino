#define SIGNAL_PIN 4 //PIR sensor
#define LED_PIN 13 //LED light

void setup()
{
  Serial.begin(115200);

  pinMode(SIGNAL_PIN, INPUT);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, LOW);
}

void loop() {
  if (digitalRead(SIGNAL_PIN) == HIGH) {
    Serial.println("Movement detected.");
    digitalWrite(LED_PIN, HIGH);
  }
  else {
    Serial.println("Did not detect movement.");
    digitalWrite(LED_PIN, LOW);
  }
  delay(5000);
}
