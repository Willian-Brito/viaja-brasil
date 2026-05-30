import { useEffect, useState } from 'react';

import {
    useNavigate
} from 'react-router-dom';

import Navbar from '../components/Navbar';

import {
    getStates,
    getCities
} from '../api/stateApi';

import {
    createTouristSpot
} from '../api/touristSpotApi';

export default function TouristSpotCreatePage() {
    const navigate = useNavigate();

    const [states, setStates] = useState([]);
    const [cities, setCities] = useState([]);

    const [selectedState,
        setSelectedState] = useState('');

    const [loading,
        setLoading] = useState(false);

    const [error,
        setError] = useState('');

    const [form, setForm] = useState({
        name: '',
        description: '',
        location: '',
        cityIbgeCode: ''
    });

    useEffect(() => {
        loadStates();
    }, []);

    const loadStates = async () => {
        try {
            const data = await getStates();
            setStates(data);
        }
        catch {
            setError(
                'Não foi possível carregar os estados.'
            );
        }
    };

    const handleStateChange =
        async state => {

        setSelectedState(state);

        setForm({
            ...form,
            cityIbgeCode: ''
        });

        try {

            const data =
                await getCities(state);

            setCities(data);

        } catch {

            setError(
                'Não foi possível carregar as cidades.'
            );

        }
    };

    const handleSubmit =
        async event => {

        event.preventDefault();

        try {

            setLoading(true);
            setError('');

            await createTouristSpot(form);

            navigate('/');

        } catch {

            setError(
                'Não foi possível cadastrar o ponto turístico.'
            );

        } finally {

            setLoading(false);

        }
    };

    return (
        <div className="container py-4">

            <Navbar />

            <div className="row justify-content-center">

                <div className="col-md-8">

                    <div className="card shadow">

                        <div className="card-body">

                            <h2 className="mb-4">
                                Novo Ponto Turístico
                            </h2>

                            {error && (
                                <div className="alert alert-danger">
                                    {error}
                                </div>
                            )}

                            <form onSubmit={handleSubmit}>

                                <div className="mb-3">

                                    <label className="form-label">
                                        Nome
                                    </label>

                                    <input
                                        type="text"
                                        className="form-control"
                                        maxLength={150}
                                        value={form.name}
                                        onChange={e =>
                                            setForm({
                                                ...form,
                                                name: e.target.value
                                            })
                                        }
                                        required
                                    />

                                </div>

                                <div className="mb-3">

                                    <label className="form-label">
                                        Descrição
                                    </label>

                                    <textarea
                                        className="form-control"
                                        rows="4"
                                        maxLength={100}
                                        value={form.description}
                                        onChange={e =>
                                            setForm({
                                                ...form,
                                                description:
                                                    e.target.value
                                            })
                                        }
                                        required
                                    />

                                    <div className="form-text">
                                        {form.description.length}/100
                                    </div>

                                </div>

                                <div className="mb-3">

                                    <label className="form-label">
                                        Localização
                                    </label>

                                    <input
                                        type="text"
                                        className="form-control"
                                        maxLength={200}
                                        value={form.location}
                                        onChange={e =>
                                            setForm({
                                                ...form,
                                                location:
                                                    e.target.value
                                            })
                                        }
                                        required
                                    />

                                </div>

                                <div className="row">

                                    <div className="col-md-4 mb-3">

                                        <label className="form-label">
                                            Estado
                                        </label>

                                        <select
                                            className="form-select"
                                            value={selectedState}
                                            onChange={e =>
                                                handleStateChange(
                                                    e.target.value
                                                )
                                            }
                                            required
                                        >
                                            <option value="">
                                                Selecione
                                            </option>

                                            {states.map(
                                                state => (
                                                    <option
                                                        key={state}
                                                        value={state}
                                                    >
                                                        {state}
                                                    </option>
                                                )
                                            )}
                                        </select>

                                    </div>

                                    <div className="col-md-8 mb-3">

                                        <label className="form-label">
                                            Cidade
                                        </label>

                                        <select
                                            className="form-select"
                                            value={form.cityIbgeCode}
                                            onChange={e =>
                                                setForm({
                                                    ...form,
                                                    cityIbgeCode:
                                                        e.target.value
                                                })
                                            }
                                            required
                                        >
                                            <option value="">
                                                Selecione
                                            </option>

                                            {cities.map(city => (
                                                <option
                                                    key={city.ibgeCode}
                                                    value={city.ibgeCode}
                                                >
                                                    {city.name}
                                                </option>
                                            ))}
                                        </select>

                                    </div>

                                </div>

                                <div className="d-flex justify-content-end gap-2 mt-4">

                                    <button
                                        type="button"
                                        className="btn btn-secondary"
                                        onClick={() => navigate('/')}
                                    >
                                        Voltar
                                    </button>

                                    <button
                                        type="submit"
                                        className="btn btn-primary"
                                        disabled={loading}
                                    >
                                        {loading
                                            ? 'Salvando...'
                                            : 'Cadastrar'}
                                    </button>

                                </div>

                            </form>

                        </div>

                    </div>

                </div>

            </div>

        </div>
    );
}