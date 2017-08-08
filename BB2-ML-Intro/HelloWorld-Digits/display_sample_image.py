i = 300
digit = train_images[i]
import matplotlib.pyplot as pyplot 
pyplot.imshow(digit, cmap=pyplot.cm.binary) 
pyplot.show() 
print(train_labels[i])