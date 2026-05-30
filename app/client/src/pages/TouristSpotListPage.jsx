import { useEffect, useState } from 'react';

import Navbar from '../components/Navbar';
import SearchBar from '../components/SearchBar';
import Pagination from '../components/Pagination';
import TouristSpotCard from '../components/TouristSpotCard';

import { getTouristSpots } from '../api/touristSpotApi';

import logo from '../assets/img/logo-4.png';

export default function TouristSpotListPage() {
    const [touristSpots, setTouristSpots] = useState([]);

    const [search, setSearch] = useState('');
    const [filter, setFilter] = useState('');

    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const loadData = async () => {
        try {
            setLoading(true);
            setError('');

            const result = await getTouristSpots(
                filter,
                page,
                5
            );

            setTouristSpots(result.items);
            setTotalPages(result.totalPages);
        }
        catch {
            setError(
                'Não foi possível carregar os pontos turísticos.'
            );
        }
        finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadData();
    }, [page, filter]);

    const handleSearch = () => {
        setPage(1);
        setFilter(search.trim());
    };

    return (
        <div className="container py-4">

            <Navbar />
            
            <div className="d-flex justify-content-center align-items-center gap-3 mb-3">

                <img
                    src={logo}
                    alt="Viaja Brasil"
                    className="mb-3"
                    style={{
                        width: '100px',
                        height: '100px',
                        objectFit: 'contain'
                    }}
                />

                <div className="text-start">

                    <h1 className="display-5 fw-bold mb-1">
                        Viaja Brasil
                    </h1>

                    <p className="text-muted mb-0">
                        Encontre os melhores pontos turísticos do Brasil
                    </p>

                </div>
            </div>

            <SearchBar
                value={search}
                onChange={setSearch}
                onSearch={handleSearch}
            />

            {loading && (
                <div className="text-center my-5">
                    <div
                        className="spinner-border"
                        role="status"
                    >
                        <span className="visually-hidden">
                            Carregando...
                        </span>
                    </div>
                </div>
            )}

            {!loading && error && (
                <div className="alert alert-danger">
                    {error}
                </div>
            )}

            {!loading &&
                !error &&
                touristSpots.length === 0 && (
                    <div className="alert alert-warning">
                        Nenhum ponto turístico encontrado.
                    </div>
                )}

            {!loading &&
                !error &&
                touristSpots.map(item => (
                    <TouristSpotCard
                        key={item.id}
                        touristSpot={item}
                    />
                ))}

            {!loading &&
                !error &&
                totalPages > 1 && (
                    <div className="mt-4">
                        <Pagination
                            currentPage={page}
                            totalPages={totalPages}
                            onPageChange={setPage}
                        />
                    </div>
                )}

        </div>
    );
}