﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class NegocioArticulo
    {
        public List<Articulo> ListarArticulos()
        {
            Articulo aux;
            List<Articulo> ListarArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearQuery("select A.Id, A.Codigo, A.Nombre, A.Descripcion, m.Id, m.Descripcion, c.Id, c.Descripcion, A.ImagenURL, A.Precio from ARTICULOS A inner join MARCAS AS M ON A.IdMarca = M.Id inner join CATEGORIAS AS C ON A.IdCategoria = C.Id");
                datos.EjecutarConsulta();
                
                while(datos.Lector.Read())
                {
                    aux = new Articulo();
                    aux.ID = datos.Lector.GetInt32(0);
                    aux.Codigo = datos.Lector.GetString(1);
                    aux.Nombre = datos.Lector.GetString(2);
                    aux.Descripcion = datos.Lector.GetString(3);

                    aux.Marca = new Marca();
                    aux.Marca.ID = datos.Lector.GetInt32(4);
                    aux.Marca.Descripcion = datos.Lector.GetString(5);

                    aux.Categoria = new Categoria();
                    aux.Categoria.ID = datos.Lector.GetInt32(6);
                    aux.Categoria.Descripcion = datos.Lector.GetString(7);

                    aux.ImagenUrl = datos.Lector.GetString(8);
                    aux.Precio = datos.Lector.GetDecimal(9);

                    ListarArticulos.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListarArticulos;
        }

        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.SetearQuery("insert into ARTICULOS (Codigo, Nombre, Descripcion,IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @Categoria, @Marca, @ImagenUrl, @Precio);");
            datos.AgregarParametro("@Codigo", nuevo.Codigo);
            datos.AgregarParametro("@Nombre", nuevo.Nombre);
            datos.AgregarParametro("@Descripcion", nuevo.Descripcion);
            datos.AgregarParametro("@Categoria", nuevo.Categoria.ID);
            datos.AgregarParametro("@Marca", nuevo.Marca.ID);
            datos.AgregarParametro("@ImagenUrl", nuevo.ImagenUrl);
            datos.AgregarParametro("@Precio", nuevo.Precio);
            datos.EjecutarConsulta();
        }

        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearQuery("Update ARTICULOS Set Codigo=@codigo, Nombre=@Nombre, Descripcion=@Descripcion, IDCategoria=@idCategoria, IDMarca=@idMarca,ImagenURL=@imagenURL, Precio=@precio where ID=@id");
                datos.AgregarParametro("@codigo", articulo.Codigo);
                datos.AgregarParametro("@Nombre", articulo.Nombre);
                datos.AgregarParametro("@Descripcion", articulo.Descripcion);
                datos.AgregarParametro("@idCategoria", articulo.Categoria.ID);
                datos.AgregarParametro("@idMarca", articulo.Marca.ID);
                datos.AgregarParametro("@imagenURL", articulo.ImagenUrl);
                datos.AgregarParametro("@precio", articulo.Precio);
                datos.AgregarParametro("@id", articulo.ID);
                datos.EjecutarConsulta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearQuery("Delete from ARTICULOS where ID=@id ");
                datos.AgregarParametro("@id",id);
                datos.EjecutarConsulta();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }  
    }
}
