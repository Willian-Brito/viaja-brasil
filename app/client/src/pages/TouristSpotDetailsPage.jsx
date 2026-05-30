import { useEffect, useState } from 'react';

import {
    Link,
    useParams
} from 'react-router-dom';

import Navbar from '../components/Navbar';

import { getTouristSpot } from '../api/touristSpotApi';

export default function TouristSpotDetailsPage() {
    const { id } = useParams();

    const [touristSpot, setTouristSpot] =
        useState(null);

    const [loading, setLoading] =
        useState(true);

    const [error, setError] =
        useState('');

    useEffect(() => {
        loadTouristSpot();
    }, []);

    const loadTouristSpot = async () => {
        try {
            setLoading(true);
            setError('');

            const result =
                await getTouristSpot(id);

            setTouristSpot(result);
        }
        catch {
            setError(
                'Não foi possível carregar o ponto turístico.'
            );
        }
        finally {
            setLoading(false);
        }
    };

    return (
        <div className="container py-4">

            <Navbar />

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
                touristSpot && (
                    <div className="card shadow">

                        <div className="card-body">

                            <h2 className="card-title mb-4">
                                {touristSpot.name}
                            </h2>

                            <p>
                                <strong>
                                    Descrição:
                                </strong>
                                <br />
                                {touristSpot.description}
                            </p>

                            <hr />

                            <p>
                                <strong>
                                    📍 Localização
                                </strong>
                                <br />
                                {touristSpot.location}
                            </p>

                            <p>
                                <strong>
                                    🏙 Cidade
                                </strong>
                                <br />
                                {touristSpot.city}
                            </p>

                            <p>
                                <strong>
                                    🌎 Estado
                                </strong>
                                <br />
                                {touristSpot.state}
                            </p>

                            <p>
                                <strong>
                                    📅 Data de inclusão
                                </strong>
                                <br />
                                {new Date(
                                    touristSpot.createdAt
                                ).toLocaleDateString(
                                    'pt-BR'
                                )}
                            </p>

                            <div className="mt-4">

                                <Link
                                    to="/"
                                    className="btn btn-secondary"
                                >
                                    Voltar
                                </Link>

                            </div>

                        </div>

                    </div>
                )}

        </div>
    );
}