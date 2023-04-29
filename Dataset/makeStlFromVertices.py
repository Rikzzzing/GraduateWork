import numpy as np
from stl import mesh
import matplotlib.pyplot as plt
from scipy.spatial import ConvexHull
from scipy.spatial import Delaunay
import pathlib as pl

# config
projection = 'Z'    # Проекция
method = 0          # 0 - триангуляция Делоне, 1 - выпуклая оболочка
current_path = pl.Path(__file__).parent.resolve()
open_file_path = current_path.joinpath("Cube_122.txt") # набор точек
save_file_path = current_path.joinpath("object.stl")         # 

points_src = []     # все точки из исходного файла
points = []         # набор точек без повторов

# Площадь симплекса в 3D
def get_area(simplex):
    area = 0.5 * (simplex[0][0] * (simplex[1][1] - simplex[2][1])
                + simplex[1][0] * (simplex[2][1] - simplex[0][1])
                + simplex[2][0] * (simplex[0][1] - simplex[1][1])
    )
    return abs(area)

# ==============================================================
# # === From file
# исходный файл с вершинами в формате:
# x1, y1, z1
# x2, y2, z2
# 
# xn, yn, zn

file = open(open_file_path)

for line in file:
        points_src.append([float(x) for x in line.split(', ')])

# Убрать повторяющиеся точки
for pnt in points_src:
    if pnt not in points:
        points.append(pnt)
# print(points)
# exit()
# ==============================================================
vertices = np.array(points)  # numpy массив вершин

if(method == 0):

    vertices_2d = np.zeros((vertices.shape[0],2))   # numpy массив вершин 2D

    for i in range (vertices.shape[0]):
        vertex = np.array(vertices[i,:])

        if(projection == 'X'):
            vertices_2d[i][0] = vertex[1]
            vertices_2d[i][1] = vertex[2]

        if(projection == 'Y'):
            vertices_2d[i][0] = vertex[0]
            vertices_2d[i][1] = vertex[2]

        if(projection == 'Z'):
            vertices_2d[i][0] = vertex[0]
            vertices_2d[i][1] = vertex[1]

    # -----------------------------------------

    tri = Delaunay(vertices_2d) # триангуляция Делоне

    new_simplices = []

    simplex = np.zeros((3, 2))

    # исключить симплексы с большой площадью
    for i, f in enumerate(tri.simplices):
        for j in range(3):
            simplex[j] = vertices_2d[f[j],:]
        area = get_area(simplex)
        print("simplex_area: " + str(area))
        # if(area < 7.0e-5):
        new_simplices.append(tri.simplices[i])

    rr = np.array(new_simplices)

    # +++++++++++++++++++++++++++++++++++++
    # plt.triplot(vertices_2d[:,0], vertices_2d[:,1], tri.simplices.copy())
    plt.triplot(vertices_2d[:,0], vertices_2d[:,1], rr.copy())
    plt.plot(vertices_2d[:,0], vertices_2d[:,1], 'o')
    plt.show()
    # -----------------------------------------
    faces = rr # ?????????

if(method == 1):
    hull = ConvexHull(vertices, incremental = True, qhull_options="Qa")
    faces = hull.simplices
    #faces = tri.simplices

# Create the mesh
cube = mesh.Mesh(np.zeros(faces.shape[0], dtype=mesh.Mesh.dtype))
for i, f in enumerate(faces):
    for j in range(3):
        cube.vectors[i][j] = vertices[f[j],:]

# Сохранить в файл
cube.save(save_file_path)
