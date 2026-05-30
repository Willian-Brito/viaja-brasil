import { Link } from 'react-router-dom';

export default function Header() {
    return (
        <div className="d-flex justify-content-between align-items-center mb-4">
            <Link
                to="/"
                className="text-decoration-none"
            >
                <h2>🇧🇷 ViajaBrasil</h2>
            </Link>

            <Link
                to="/create"
                className="btn btn-primary"
            >
                Cadastrar Ponto Turístico
            </Link>
        </div>
    );
}