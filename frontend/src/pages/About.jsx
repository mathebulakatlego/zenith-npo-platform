
import PageContainer from '../components/layout/PageContainer'
import Section from '../components/layout/Section'

function About() {
  return (
    <main>
      <section className="page-hero">
        <PageContainer>
          <div className="page-hero__content">
            <h1>About Zenith</h1>

            <p>
              Zenith Future Skills Hub exists to help young people build
              practical skills, discover opportunities and develop the
              confidence to shape their futures.
            </p>
          </div>
        </PageContainer>
      </section>

      <Section>
        <PageContainer>
          <div className="about-split">
            <div>
              <h2 className="section-eyebrow">Our Purpose</h2>
            </div>

            <div>
              <p>
                We believe that access to practical skills can open doors to
                meaningful opportunities. Zenith creates accessible learning
                experiences that help young people develop skills they can
                apply in the real world. Our work brings technology, practical
                learning, mentorship and career preparation together to help
                learners take their next step with greater confidence.
              </p>
            </div>
          </div>
        </PageContainer>
      </Section>

      <Section className="section--light">
        <PageContainer>
          <div className="section-heading">
            <h2 className="section-eyebrow">What We Do</h2>
          </div>

          <div className="about-pillars">
            <article className="about-pillar">
              <h3>Practical Skills</h3>
              <p>
                We focus on useful digital and technical skills that learners
                can practise, build with and carry into future opportunities.
              </p>
            </article>

            <article className="about-pillar">
              <h3>Career Readiness</h3>
              <p>
                Learning goes beyond technical knowledge. We help learners
                develop the confidence and preparation needed to pursue
                opportunities.
              </p>
            </article>

            <article className="about-pillar">
              <h3>Growth &amp; Opportunity</h3>
              <p>
                We aim to create pathways where skills can become a foundation
                for continued learning, employment and entrepreneurship.
              </p>
            </article>
          </div>
        </PageContainer>
      </Section>

      <Section>
        <PageContainer>
          <div className="section-heading">
            <h2 className="section-eyebrow">Our Team</h2>
          </div>

          <div className="team-grid">
            <article className="team-card">
              <div className="team-card__placeholder" aria-hidden="true">
                KM
              </div>

              <h3>Katlego Mathebula</h3>
              <p>Chairperson</p>
            </article>

            <article className="team-card">
              <div className="team-card__placeholder" aria-hidden="true">
                GD
              </div>

              <h3>Grace</h3>
              <p>Deputy Chairperson</p>
            </article>

            <article className="team-card">
              <div className="team-card__placeholder" aria-hidden="true">
                VT
              </div>

              <h3>Vincent</h3>
              <p>Treasurer</p>
            </article>

            <article className="team-card">
              <div className="team-card__placeholder" aria-hidden="true">
                TS
              </div>

              <h3>Tshegofatso</h3>
              <p>Secretary</p>
            </article>

            <article className="team-card">
              <div className="team-card__placeholder" aria-hidden="true">
                MF
              </div>

              <h3>Mary</h3>
              <p>Facilitator</p>
            </article>
          </div>
        </PageContainer>
      </Section>

      <Section className="about-cta">
        <PageContainer>
          <div className="about-cta__content">
            <h2 className="section-eyebrow">Explore Zenith</h2>

            <a className="btn btn--primary" href="/programmes">
              Explore Programmes
            </a>
          </div>
        </PageContainer>
      </Section>
    </main>
  )
}

export default About

