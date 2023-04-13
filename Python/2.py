import numpy
from PIL import Image

# img = Image.open(r'E:\Unity\DiplomRaycast\Python\Cube_1.png')
# img.show()

# print(img.size)
# print(img.format)
# print(img.mode)
# print(img.histogram)

# img.convert(mode = 'L').show()
# img = numpy.array(img)
# print(img)

def read_X_train():
    i = 1

    while i < 2:
        img = Image.open(r'E:\Unity\DiplomRaycast\Dataset\DatasetScreenshots\Cube_%d.png' % i)
        X_train = numpy.array(img)
        i += 1

    return X_train

X = read_X_train()
# X = X.reshape(1000, 1000, 1)
# print(X_train.shape())
print(X)