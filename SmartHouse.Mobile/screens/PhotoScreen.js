import React from 'react';
import { View, Text, StyleSheet } from 'react-native';

const PhotoScreen = () => {
    return (
      <View style={styles.container}>
        <Text>Photo Screen</Text>
      </View>
    );
};

export default PhotoScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1, 
    alignItems: 'center', 
    justifyContent: 'center'
  },
});