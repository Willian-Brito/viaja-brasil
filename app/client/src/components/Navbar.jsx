import { Link } from 'react-router-dom';
import logo from '../assets/img/logo-4.png';

export default function Navbar() {
    return (
        <nav className="navbar navbar-expand-lg mb-4">
            <div className="container">

                <Link
                    to="/"
                    className="navbar-brand fw-bold d-flex align-items-center gap-2"
                >
                    <img
                        src={logo}
                        alt="ViajaBrasil"
                        style={{
                            height: '48px',
                            width: '48px',
                            objectFit: 'contain'
                        }}
                    />

                    <span>
                        ViajaBrasil
                    </span>
                </Link>

                <Link
                    to="/create"
                    className="btn btn-primary"
                >
                    Cadastrar Ponto Turístico
                </Link>

            </div>
        </nav>
    );
}