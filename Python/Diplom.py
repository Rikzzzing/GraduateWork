# импорт библиотек (tkinter - для создания пользовательского интерфейса,
# tensorflow - для работы с нейросетью, numpy - для математических операций)
import numpy
import tkinter
import tensorflow
from tkinter import ttk
from tkinter import font
from tkinter import filedialog
from PIL import Image, ImageTk

# процедура открытия файла (картинки)
def OpenFile():
    global filepath
    global imgTK
    global img

    filepath = filedialog.askopenfilename(filetypes=[("JPEG Files", "*.jpg")])
    if not filepath:
        return
    
    img = Image.open(filepath)

    width = img.size[0]
    height = img.size[1]
    xFactor = width / imageViewer.winfo_width()
    yFactor = height / imageViewer.winfo_height()
    
    if (xFactor > 1 or yFactor > 1):
        maxFactor = max(xFactor, yFactor)
        img = img.resize((int(width / maxFactor), int(height / maxFactor)))

    imgTK = ImageTk.PhotoImage(img)
    img = imageViewer.create_image(0, 0, anchor = 'nw', image = imgTK)

# процедура построения 3D модели
def Processing():
    print("processing...")

# создание главного окна
window = tkinter.Tk()
window.title("3D object from image")    # заголовок окна
window.geometry("1000x600")             # размер окна по умолчанию
window.minsize(1000, 600)               # минимальный размер окна
window.rowconfigure(0, weight = 1)
window.columnconfigure(1, weight = 1)

# создание фреймов для разделения рабочей области окна
frameMenu = ttk.Frame(window, borderwidth = 3, relief = "solid" , padding = [8, 10])
frameOpenButton = ttk.Frame(frameMenu, relief = "solid")
frameAnotherButton = ttk.Frame(frameMenu, relief = "solid")
frameResult = ttk.Frame(window, borderwidth = 3, relief = "solid" , padding = [8, 10])

frameOpenButton.columnconfigure(0, weight = 1)
frameAnotherButton.columnconfigure(0, weight = 1)

# создание стилей
fontForButton = font.Font(size = 16)
buttonStyle = ttk.Style()
buttonStyle.configure("TButton", font = fontForButton, padding = [0, 5])

# создание кнопок (открытие файла, построение 3D модели и выход)
buttonOpen = ttk.Button(master = frameOpenButton, text = "Open File", command = OpenFile)
buttonStart = ttk.Button(master = frameAnotherButton, text = "Start", command = Processing)
buttonExit = ttk.Button(master = frameAnotherButton, text = "Exit", command = window.quit)

# создание окна предпросмотра
imageViewer = tkinter.Canvas(frameOpenButton, height = 400, width = 400, borderwidth = 1, relief = "solid")

# расположение элементов в фреймах
buttonOpen.grid(row = 0, column = 0, sticky = "ew")
imageViewer.grid(row = 1, column = 0)
buttonStart.grid(row = 0, column = 0, sticky = "ew")
buttonExit.grid(row = 1, column = 0, sticky = "ew")

# размещение фреймов
frameMenu.grid(row = 0, column = 0, sticky = "ns", padx = 20, pady = 20)
frameOpenButton.grid(sticky = "ew")
frameAnotherButton.grid(sticky = "ew")
frameResult.grid(row = 0, column = 1, sticky = "nsew", padx = 20, pady = 20)

window.mainloop()