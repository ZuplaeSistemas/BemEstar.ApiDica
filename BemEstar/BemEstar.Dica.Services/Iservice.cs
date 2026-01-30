namespace BemEstar.Dica.Services;

public interface Iservice<T>
{
    int Create(T model);
    List<T> Read();   
    T ReadById(int id);
    void Update(T model);
    void Delete(int id);

    bool Exists(int id);
}
