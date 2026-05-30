export default function SearchBar({
    value,
    onChange,
    onSearch
}) {
    const handleSubmit = event => {
        event.preventDefault();
        onSearch();
    };

    return (
        <form
            onSubmit={handleSubmit}
            className="row justify-content-center mb-4"
        >
            <div className="col-md-8">

                <div className="input-group">

                    <input
                        type="text"
                        className="form-control"
                        placeholder="Pesquisar ponto turístico..."
                        value={value}
                        onChange={event =>
                            onChange(event.target.value)
                        }
                    />

                    <button
                        type="submit"
                        className="btn btn-primary"
                    >
                        Buscar
                    </button>

                </div>

            </div>
        </form>
    );
}