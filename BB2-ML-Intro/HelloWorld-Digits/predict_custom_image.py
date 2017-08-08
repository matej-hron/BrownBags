img_path = 'c:\\Users\\m.hron\\Source\\Repos\\BrownBags\\BB2-ML-Intro\\HelloWorld-Digits\\data\\4.jpg'
from keras.preprocessing import image
import numpy as np

img = image.load_img(img_path, target_size=(28, 28), grayscale=True)

digit = img
import matplotlib.pyplot as pyplot 
pyplot.imshow(digit, cmap=pyplot.cm.binary) 
pyplot.show() 

img_tensor = image.img_to_array(img)
img_tensor = img_tensor.reshape(28 * 28)
img_tensor = np.expand_dims(img_tensor, axis=0)

img_tensor /= 255.


print(img_tensor.shape)
network.predict(img_tensor)