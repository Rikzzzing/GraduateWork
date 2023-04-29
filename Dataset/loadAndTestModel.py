import sys
import numpy as np
import matplotlib
import tensorflow
import keras

from PIL import Image
import matplotlib.pyplot as plt


width = 512
height = 512
pixel_matrix = np.zeros((width, height), dtype=np.uint8)

image = Image.open("Cube_42.png")
pix = image.load()

for x in range(width):
    for y in range(height):
        r, g, b = pix[x, y][0:3]
        grayscale_pixel = 0.299 * r + 0.587 * g + 0.114 * b
        pixel_matrix[y][x] = grayscale_pixel

image.close()

# plt.imshow(pixel_matrix)
# plt.show()
# exit()
pixel_matrix = pixel_matrix.reshape(1, 512, 512, 1)
pixel_matrix = pixel_matrix.astype('float32')
pixel_matrix /= 255.0
print('pixel_matrix.shape: ', pixel_matrix.shape)
# exit()

# arrays = np.load("dataset.npz")
# X_test = arrays.f.arr_2
# X_test = X_test.reshape(X_test.shape[0], 512, 512, 1)
# X_test = X_test.astype('float32')
# X_test /= 255.0


# загрузка модли
model = keras.models.load_model("model")

# оценка модели на тестовых данных
print('\n# Генерируем прогнозы')
# predictions = model.predict(X_test[:10])
# print('прогноз:', predictions[:10])

predictions = model.predict(pixel_matrix)
print('прогноз:', predictions)
# print('ответ:', Y_test[:10])